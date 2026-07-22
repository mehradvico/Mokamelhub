using Application.Services.Order.SnappPaySrv.Dto;
using Application.Services.Order.SnappPaySrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Order.SnappPaySrv
{
    public class SnappPayOrderBuilder : ISnappPayOrderBuilder
    {
        private readonly IDataBaseContext _context;
        private readonly SnappPayOptions _options;

        public SnappPayOrderBuilder(IDataBaseContext context, IOptions<SnappPayOptions> options)
        {
            _context = context;
            _options = options.Value;
        }

        public Task<ProductOrder> LoadOrderAsync(string orderId, bool tracking = false)
        {
            var query = _context.ProductOrders
                .Include(x => x.User)
                .Include(x => x.ProductOrderStores)
                    .ThenInclude(x => x.ProductOrderItems)
                        .ThenInclude(x => x.ProductItem)
                            .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Category)
                .AsQueryable();

            if (tracking)
                query = query.AsTracking();

            return query.FirstOrDefaultAsync(x => x.Id == orderId && !x.Deleted);
        }

        public SnappPayOrderPayload Build(ProductOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var request = new SnappPayOrderRequest();
            long itemDiscountRial = 0;

            foreach (var store in order.ProductOrderStores?.Where(x => !x.Deleted) ?? Enumerable.Empty<ProductOrderStore>())
            {
                var cart = new SnappPayCartRequest
                {
                    CartId = store.Id,
                    IsShipmentIncluded = false,
                    IsTaxIncluded = true,
                    ShippingAmount = ToRial(store.DeliveryPrice),
                    TaxAmount = 0
                };

                foreach (var item in store.ProductOrderItems?.Where(x => !x.Deleted && x.Count > 0) ?? Enumerable.Empty<ProductOrderItem>())
                {
                    var product = item.ProductItem?.Product;
                    var baseAmountRial = ToRial(item.BasePrice);
                    cart.CartItems.Add(new SnappPayCartItemRequest
                    {
                        Id = item.Id,
                        Amount = baseAmountRial,
                        Count = item.Count,
                        Name = Limit(product?.Name ?? item.Description ?? $"محصول {item.ProductItemId}", 255),
                        Category = Limit(product?.Category?.Name ?? "مکمل", 255),
                        CommissionType = _options.CommissionType > 0 ? _options.CommissionType : 100
                    });

                    itemDiscountRial += ToRial(Math.Max(0, item.BasePrice - item.Price) * item.Count);
                }

                if (cart.CartItems.Count == 0)
                    continue;

                cart.TotalAmount = checked(cart.CartItems.Sum(x => checked(x.Amount * x.Count)) + cart.ShippingAmount + cart.TaxAmount);
                request.CartList.Add(cart);
            }

            if (request.CartList.Count == 0)
                throw new InvalidOperationException("سفارش هیچ آیتم فعالی برای ارسال به اسنپ‌پی ندارد.");

            var totalRial = request.CartList.Sum(x => x.TotalAmount);
            request.DiscountAmount = checked(itemDiscountRial + ToRial(Math.Max(0, order.RebatePrice)));
            request.DiscountAmount = Math.Min(request.DiscountAmount, totalRial);

            var afterDiscount = totalRial - request.DiscountAmount;
            request.ExternalSourceAmount = 0;
            request.Amount = afterDiscount - request.ExternalSourceAmount;

            if (request.Amount <= 0)
                throw new InvalidOperationException("مبلغ قابل پرداخت سفارش برای اسنپ‌پی باید بیشتر از صفر باشد.");

            return new SnappPayOrderPayload
            {
                Request = request,
                ExpectedAmountRial = request.Amount
            };
        }

        public void Recalculate(ProductOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            foreach (var store in order.ProductOrderStores ?? Enumerable.Empty<ProductOrderStore>())
            {
                var activeItems = store.ProductOrderItems?.Where(x => !x.Deleted && x.Count > 0).ToList()
                    ?? new System.Collections.Generic.List<ProductOrderItem>();

                store.Deleted = activeItems.Count == 0;
                store.BasePrice = activeItems.Sum(x => x.BasePrice * x.Count);
                store.Price = activeItems.Sum(x => x.Price * x.Count);
                store.DiscountPrice = Math.Max(0, store.BasePrice - store.Price);
                store.PaymentPrice = store.Deleted ? 0 : store.Price + store.DeliveryPrice;
                store.Edited = true;
            }

            var activeStores = order.ProductOrderStores?.Where(x => !x.Deleted).ToList()
                ?? new System.Collections.Generic.List<ProductOrderStore>();

            order.BasePrice = activeStores.Sum(x => x.BasePrice);
            order.Price = activeStores.Sum(x => x.Price);
            order.DiscountPrice = Math.Max(0, order.BasePrice - order.Price);
            order.DeliveryPrice = activeStores.Sum(x => x.DeliveryPrice);
            order.RebatePrice = Math.Min(Math.Max(0, order.RebatePrice), order.Price + order.DeliveryPrice);
            order.PaymentPrice = Math.Max(0, order.Price + order.DeliveryPrice - order.RebatePrice);
        }

        private static long ToRial(double toman)
        {
            if (double.IsNaN(toman) || double.IsInfinity(toman) || toman < 0)
                throw new InvalidOperationException("مبلغ نامعتبر در سفارش ثبت شده است.");

            return checked((long)Math.Round((decimal)toman * 10m, MidpointRounding.AwayFromZero));
        }

        private static string Limit(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();
            return value.Length <= maxLength ? value : value[..maxLength];
        }
    }
}
