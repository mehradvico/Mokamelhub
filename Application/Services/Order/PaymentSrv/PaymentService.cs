using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Enumerable.Code;
using Application.Common.Service;
using Application.Services.Order.MerchantSrv.Iface;
using Application.Services.Order.PaymentSrv.Dto;
using Application.Services.Order.PaymentSrv.Iface;
using Application.Services.Order.ProductOrderSrv.Dto;
using Application.Services.Order.ProductOrderSrv.Iface;
using Application.Services.ProductSrvs.WalletSrv.IFace;
using Application.Services.Setting.CodeSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Order.PaymentSrv
{
    public class PaymentService : CommonSrv<Payment, PaymentDto>, IPaymentService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IMerchantService _merchantService;
        private readonly IProductOrderService _productOrderService;
        private readonly IWalletService _walletService;
        private readonly ICodeService _codeService;

        public PaymentService(IDataBaseContext _context, IMapper mapper, ICodeService codeService, IWalletService walletService, IMerchantService merchantService, IProductOrderService productOrderService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            _merchantService = merchantService;
            _productOrderService = productOrderService;
            _walletService = walletService;
            _codeService = codeService;       
        }

        public async Task<BaseResultDto> InsertWalletPaymentAsyncDto(PaymentStartDto dto)
        {
            if (dto.Amount < 10000)
            {
                dto.Amount = 10000;
            }
            else if (dto.MerchantId == null)
            {
                return new BaseResultDto(false, Resource.Notification.PleaseSelectTheMerchant);

            }
            var paymentTypeWallet = await _codeService.GetByLabelAsync(PaymentTypeEnum.PaymentType_Wallet.ToString());
            dto.IsOnline = true;
            dto.ProductOrderId = null;
            dto.TypeId = paymentTypeWallet.Id;
            return await StartPayment(dto);

        }

        public async Task<BaseResultDto> StartPayment(PaymentStartDto dto)
        {
            try
            {
                if (dto.Amount < 10000)
                {
                    return new BaseResultDto(false, string.Format(Resource.Pattern.AmountsLessT1CannotPaid, 10000));

                }
                var item = mapper.Map<Payment>(dto);
                DateTime justNow = DateTime.Now;
                item.CreateDate = justNow;
                item.IsOnline = true;
                await _context.Payments.AddAsync(item);
                await _context.SaveChangesAsync();
                dto.PaymentId = item.Id;
                dto.CallbackUrl =$"https://payment.pastil.pet/callback/{item.Id}";
                var initPayment = await _merchantService.StartAsync(dto);
                if (!initPayment.IsSuccess)
                {
                    item.IsSuccess = false;
                    item.Description = string.Format("{0}:{1}", Resource.Notification.ErrorOnStartPayment, initPayment.Messages);
                    _context.Payments.Update(item);
                    await _context.SaveChangesAsync(true);
                }
                return initPayment;
            }
            catch (Exception ex)
            {
                return new BaseResultDto(isSuccess: false, val: ex.Message);
            }
        }
        public async Task<BaseResultDto<PaymentDto>> CallbackPayment(long paymentId, bool test = false)
        {
            try
            {
                var payment = await FindAsync(paymentId);
                if (payment == null)
                {
                    return new BaseResultDto<PaymentDto>(isSuccess: false, val: Resource.Notification.Unsuccess, null);
                }
                if (payment.IsSuccess != null)
                {
                    return new BaseResultDto<PaymentDto>(isSuccess: false, val: Resource.Notification.Unsuccess, null);
                }
                else
                {
                    var callback = await _merchantService.CallbackAsync(payment, test);
                    if (callback.IsSuccess)
                    {
                        if (payment.Type.Label == PaymentTypeEnum.PaymentType_ProductOrder.ToString())
                        {
                            if (payment.CallBackTypeLabel == PaymentCallbackTypeEnum.ProductOrder.ToString())
                            {
                                var productPaymentCallback = await _productOrderService.ProductPaymentCallback(payment.ProductOrderId, fromWallet: true);
                                if (!productPaymentCallback.IsSuccess)
                                {
                                    return new BaseResultDto<PaymentDto>(isSuccess: false, val: Resource.Notification.Unsuccess, null);

                                }
                            }
                        }
                        else if (payment.Type.Label == PaymentTypeEnum.PaymentType_Wallet.ToString())
                        {
                            await _walletService.WalletPaymentCallback(payment);
                        }

                    }
                    else
                    {
                        if (payment.Type.Label == PaymentTypeEnum.PaymentType_ProductOrder.ToString())
                        {
                            await _productOrderService.UpdateWalletAsync(payment.ProductOrderId, false);

                        }

                    }
                    return new BaseResultDto<PaymentDto>(isSuccess: callback.IsSuccess, data: mapper.Map<PaymentDto>(payment));

                }
            }
            catch (Exception ex)
            {
                return new BaseResultDto<PaymentDto>(isSuccess: false, val: ex.Message, null);
            }
        }
        public BaseSearchDto<PaymentVDto> Search(PaymentInputDto baseSearchDto)
        {
            var query = _context.Payments.Include(s => s.Merchant).ThenInclude(s => s.Bank).Include(s => s.File).AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.ProductOrderId))
            {
                query = query.Where(s => s.ProductOrderId == baseSearchDto.ProductOrderId);
            }
            switch (baseSearchDto.SortBy)
            {
                case Common.Enumerable.SortEnum.Default:
                    {
                        query = query.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.New:
                    {
                        query = query.OrderByDescending(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.Old:
                    {
                        query = query.OrderBy(s => s.Id);
                        break;
                    }
                case Common.Enumerable.SortEnum.Expensive:
                    {
                        query = query.OrderByDescending(s => s.Amount);
                        break;
                    }
                case Common.Enumerable.SortEnum.Inexpensive:
                    {
                        query = query.OrderBy(s => s.Amount);
                        break;
                    }
                default:
                    break;
            }

            return new BaseSearchDto<Payment, PaymentVDto>(baseSearchDto, query, mapper);
        }

        public async Task<Payment> FindAsync(long id)
        {
            return await _context.Payments.Include(s => s.User).Include(s => s.Type).Include(s => s.ProductOrder).Include(s => s.Merchant).ThenInclude(s => s.Bank).AsTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
