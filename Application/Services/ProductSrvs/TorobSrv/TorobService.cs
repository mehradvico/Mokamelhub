using Application.Common.Enumerable;
using Application.Services.ProductSrvs.TorobSrv.Dto;
using Application.Services.ProductSrvs.TorobSrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.TorobSrv
{
    public sealed class TorobService : ITorobService
    {
        private const int PageSize = 100;

        private const string SortByDateAdded = "date_added_desc";
        private const string SortByDateUpdated = "date_updated_desc";

        private readonly IDataBaseContext _context;
        private readonly string _siteBaseUrl;
        private readonly string _fileBaseUrl;
        private readonly string _defaultGuarantee;

        public TorobService(
            IDataBaseContext context,
            IConfiguration configuration
        )
        {
            _context = context;

            _siteBaseUrl =
                configuration["Torob:SiteBaseUrl"]?.TrimEnd('/')
                ?? "https://mokamelhub.com";

            _fileBaseUrl =
                configuration["Torob:FileBaseUrl"]?.TrimEnd('/')
                ?? "https://file.mokamelhub.com";

            _defaultGuarantee =
                configuration["Torob:GuaranteeText"]
                ?? "ضمانت اصالت کالا";
        }

        public async Task<TorobServiceResult> GetProductsAsync(
            TorobRequestDto request,
            CancellationToken cancellationToken = default
        )
        {
            var validationError = ValidateRequest(request);

            if (validationError != null)
            {
                return TorobServiceResult.Failure(validationError);
            }

            if (request.Page.HasValue)
            {
                return await GetPagedProductsAsync(
                    request.Page.Value,
                    request.Sort,
                    cancellationToken
                );
            }

            if (request.PageUniques != null)
            {
                return await GetByPageUniquesAsync(
                    request.PageUniques,
                    cancellationToken
                );
            }

            return await GetByPageUrlsAsync(
                request.PageUrls,
                cancellationToken
            );
        }

        private IQueryable<ProductItem> BaseFeedQuery()
        {
            return _context.ProductItems
                .IgnoreAutoIncludes()
                .AsNoTracking()
                .Where(item =>
                    !item.Deleted &&
                    item.Active &&
                    item.SystemActive &&

                    !item.Product.Deleted &&
                    item.Product.Active &&

                    item.Product.StatusId !=
                    (long)ProductStatusEnum.ProductStatus_Draft &&

                    item.Product.ProductLabel != null &&
                    item.Product.ProductLabel != ""
                );
        }

        private async Task<TorobServiceResult> GetPagedProductsAsync(
            int page,
            string sort,
            CancellationToken cancellationToken
        )
        {
            var query = BaseFeedQuery();

            var total = await query.CountAsync(cancellationToken);

            var maxPages = Math.Max(
                1,
                (int)Math.Ceiling(total / (double)PageSize)
            );

            IQueryable<ProductItem> orderedQuery;

            if (sort == SortByDateAdded)
            {
                orderedQuery = query
                    .OrderByDescending(item => item.Product.CreateDate)
                    .ThenByDescending(item => item.Id);
            }
            else
            {
                orderedQuery = query
                    .OrderByDescending(item => item.Product.UpdateDate)
                    .ThenByDescending(item => item.Id);
            }

            var skipLong = ((long)page - 1) * PageSize;

            List<long> itemIds;

            if (skipLong > int.MaxValue)
            {
                itemIds = new List<long>();
            }
            else
            {
                itemIds = await orderedQuery
                    .Skip((int)skipLong)
                    .Take(PageSize)
                    .Select(item => item.Id)
                    .ToListAsync(cancellationToken);
            }

            var items = await LoadProductItemsAsync(
                itemIds,
                cancellationToken
            );

            var products = items
                .Select(MapProduct)
                .ToList();

            return TorobServiceResult.Success(
                new TorobResponseDto
                {
                    CurrentPage = page,
                    Total = total,
                    MaxPages = maxPages,
                    Products = products
                }
            );
        }

        private async Task<TorobServiceResult> GetByPageUniquesAsync(
            IReadOnlyCollection<string> pageUniques,
            CancellationToken cancellationToken
        )
        {
            var requestedIds = pageUniques
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value =>
                {
                    var success = long.TryParse(
                        value.Trim(),
                        NumberStyles.Integer,
                        CultureInfo.InvariantCulture,
                        out var id
                    );

                    return new
                    {
                        Success = success,
                        Id = id
                    };
                })
                .Where(result => result.Success && result.Id > 0)
                .Select(result => result.Id)
                .Distinct()
                .ToList();

            var items = await LoadProductItemsAsync(
                requestedIds,
                cancellationToken
            );

            return CreateDirectResult(items);
        }

        private async Task<TorobServiceResult> GetByPageUrlsAsync(
            IReadOnlyCollection<string> pageUrls,
            CancellationToken cancellationToken
        )
        {
            var requestedUrls = pageUrls
                .Select(url => new
                {
                    ItemId = ExtractProductItemIdFromUrl(url),
                    ProductLabel = ExtractProductLabelFromUrl(url)
                })
                .ToList();

            var requestedIds = requestedUrls
                .Select(url => url.ItemId)
                .Where(id => id.HasValue && id.Value > 0)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            var requestedLabels = requestedUrls
                .Where(url => !url.ItemId.HasValue)
                .Select(url => url.ProductLabel)
                .Where(label => !string.IsNullOrWhiteSpace(label))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (requestedLabels.Count > 0)
            {
                var labelItems = await _context.ProductItems
                    .IgnoreAutoIncludes()
                    .AsNoTracking()
                    .Where(item =>
                        item.Product != null &&
                        requestedLabels.Contains(item.Product.ProductLabel)
                    )
                    .Select(item => new
                    {
                        item.Id,
                        item.Active,
                        item.SystemActive,
                        item.Deleted,
                        item.Quantity,
                        ProductLabel = item.Product.ProductLabel
                    })
                    .ToListAsync(cancellationToken);

                foreach (var label in requestedLabels)
                {
                    var matchedItem = labelItems
                        .Where(item => string.Equals(
                            item.ProductLabel,
                            label,
                            StringComparison.OrdinalIgnoreCase
                        ))
                        .OrderByDescending(item =>
                            !item.Deleted &&
                            item.Active &&
                            item.SystemActive &&
                            item.Quantity > 0
                        )
                        .ThenBy(item => item.Id)
                        .FirstOrDefault();

                    if (matchedItem != null &&
                        !requestedIds.Contains(matchedItem.Id))
                    {
                        requestedIds.Add(matchedItem.Id);
                    }
                }
            }

            var items = await LoadProductItemsAsync(
                requestedIds,
                cancellationToken
            );

            return CreateDirectResult(items);
        }

        private TorobServiceResult CreateDirectResult(
            IReadOnlyCollection<ProductItem> items
        )
        {
            var products = items
                .Select(MapProduct)
                .ToList();

            return TorobServiceResult.Success(
                new TorobResponseDto
                {
                    CurrentPage = 1,
                    Total = products.Count,
                    MaxPages = 1,
                    Products = products
                }
            );
        }

        private async Task<List<ProductItem>> LoadProductItemsAsync(
            IReadOnlyCollection<long> ids,
            CancellationToken cancellationToken
        )
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<ProductItem>();
            }

            var items = await _context.ProductItems
                .IgnoreAutoIncludes()
                .AsNoTracking()

                .Where(item => ids.Contains(item.Id))

                .Include(item => item.Product)
                    .ThenInclude(product => product.Category)

                .Include(item => item.Product)
                    .ThenInclude(product => product.Brand)

                .Include(item => item.Product)
                    .ThenInclude(product => product.Picture)

                .Include(item => item.Product)
                    .ThenInclude(product => product.ProductPictures)
                    .ThenInclude(productPicture => productPicture.Picture)

                .Include(item => item.VarietyItem)
                    .ThenInclude(varietyItem => varietyItem.Variety)

                .Include(item => item.VarietyItem2)
                    .ThenInclude(varietyItem => varietyItem.Variety)

                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            var positions = ids
                .Select((id, index) => new
                {
                    Id = id,
                    Index = index
                })
                .ToDictionary(item => item.Id, item => item.Index);

            return items
                .OrderBy(item => positions[item.Id])
                .ToList();
        }

        private TorobProductDto MapProduct(ProductItem item)
        {
            var product = item.Product;

            var titleParts = new List<string>
            {
                product.Name
            };

            if (!string.IsNullOrWhiteSpace(item.VarietyItem?.Name))
            {
                titleParts.Add(item.VarietyItem.Name);
            }

            if (!string.IsNullOrWhiteSpace(item.VarietyItem2?.Name))
            {
                titleParts.Add(item.VarietyItem2.Name);
            }

            var currentPrice = item.Price > 0
                ? item.Price
                : item.BasePrice;

            long? oldPrice = null;

            if (item.Price > 0 && item.BasePrice > item.Price)
            {
                oldPrice = item.BasePrice;
            }

            var isAvailable =
                !item.Deleted &&
                item.Active &&
                item.SystemActive &&
                item.Quantity > 0 &&
                currentPrice > 0 &&
                !product.Deleted &&
                product.Active &&
                product.StatusId ==
                (long)ProductStatusEnum.ProductStatus_Available;

            return new TorobProductDto
            {
                PageUnique = Limit(
                    item.Id.ToString(CultureInfo.InvariantCulture),
                    200
                ),

                PageUrl = Limit(
                    BuildProductUrl(product.ProductLabel, item.Id),
                    1500
                ),

                ProductGroupId = Limit(
                    product.Id.ToString(CultureInfo.InvariantCulture),
                    200
                ),

                Title = Limit(
                    string.Join(
                        " - ",
                        titleParts.Where(value =>
                            !string.IsNullOrWhiteSpace(value)
                        )
                    ),
                    500
                ),

                Subtitle = NullIfEmpty(
                    Limit(product.SecondName, 500)
                ),

                CurrentPrice = currentPrice,

                OldPrice = oldPrice,

                Availability = isAvailable,

                CategoryName = NullIfEmpty(
                    Limit(product.Category?.Name, 200)
                ),

                ImageLinks = BuildImageLinks(product),

                Spec = BuildSpecifications(item),

                Guarantee = NullIfEmpty(
                    Limit(
                        string.IsNullOrWhiteSpace(item.Warranty)
                            ? _defaultGuarantee
                            : item.Warranty,
                        200
                    )
                ),

                ShortDescription = NullIfEmpty(
                    Limit(
                        HtmlToPlainText(
                            !string.IsNullOrWhiteSpace(product.Summary)
                                ? product.Summary
                                : product.Description
                        ),
                        500
                    )
                ),

                DateAdded = ToTehranIso8601(product.CreateDate),

                DateUpdated = ToTehranIso8601(product.UpdateDate)
            };
        }

        private Dictionary<string, string> BuildSpecifications(
            ProductItem item
        )
        {
            var result = new Dictionary<string, string>();

            AddSpecification(
                result,
                "برند",
                item.Product.Brand?.Name
            );

            AddSpecification(
                result,
                item.VarietyItem?.Variety?.Name ?? "ویژگی اول",
                item.VarietyItem?.Name
            );

            AddSpecification(
                result,
                item.VarietyItem2?.Variety?.Name ?? "ویژگی دوم",
                item.VarietyItem2?.Name
            );

            return result;
        }

        private static void AddSpecification(
            IDictionary<string, string> target,
            string key,
            string value
        )
        {
            if (string.IsNullOrWhiteSpace(key) ||
                string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var finalKey = key.Trim();

            if (target.ContainsKey(finalKey))
            {
                finalKey = $"{finalKey} ۲";
            }

            target[finalKey] = value.Trim();
        }

        private List<string> BuildImageLinks(Product product)
        {
            var images = new List<string>();

            AddPicture(images, product.Picture);

            foreach (var productPicture in
                     product.ProductPictures?.OrderBy(item => item.Id)
                     ?? Enumerable.Empty<ProductPicture>())
            {
                AddPicture(images, productPicture.Picture);
            }

            return images
                .Where(url => !string.IsNullOrWhiteSpace(url))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Take(20)
                .ToList();
        }

        private void AddPicture(
            ICollection<string> target,
            Picture picture
        )
        {
            var url = BuildPictureUrl(picture);

            if (!string.IsNullOrWhiteSpace(url))
            {
                target.Add(Limit(url, 1000));
            }
        }

        private string BuildPictureUrl(Picture picture)
        {
            if (picture == null ||
                string.IsNullOrWhiteSpace(picture.Url))
            {
                return null;
            }

            var path = picture.Url.Trim();

            if (Uri.TryCreate(
                    path,
                    UriKind.Absolute,
                    out var absoluteUri
                ))
            {
                return absoluteUri.ToString();
            }

            path = path.TrimStart('/');

            return $"{_fileBaseUrl}/{path}";
        }
        private string BuildProductUrl(
            string productLabel,
            long productItemId
        )
        {
            var escapedLabel = Uri.EscapeDataString(
                productLabel?.Trim() ?? string.Empty
            );

            return
                $"{_siteBaseUrl}/products/{escapedLabel}?item={productItemId}";
        }

        private static long? ExtractProductItemIdFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url) ||
                !Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return null;
            }

            var query = uri.Query.TrimStart('?');

            foreach (var part in query.Split(
                         '&',
                         StringSplitOptions.RemoveEmptyEntries
                     ))
            {
                var segments = part.Split('=', 2);

                if (segments.Length != 2)
                {
                    continue;
                }

                var key = Uri.UnescapeDataString(segments[0]);
                var value = Uri.UnescapeDataString(segments[1]);

                if (!key.Equals(
                        "item",
                        StringComparison.OrdinalIgnoreCase
                    ))
                {
                    continue;
                }

                if (long.TryParse(
                        value,
                        NumberStyles.Integer,
                        CultureInfo.InvariantCulture,
                        out var itemId
                    ))
                {
                    return itemId;
                }
            }

            return null;
        }

        private static string ExtractProductLabelFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url) ||
                !Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return null;
            }

            var segments = uri.AbsolutePath
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

            for (var index = 0; index < segments.Length - 1; index++)
            {
                if (!segments[index].Equals(
                        "products",
                        StringComparison.OrdinalIgnoreCase
                    ))
                {
                    continue;
                }

                return Uri.UnescapeDataString(segments[index + 1])
                    .Trim();
            }

            return null;
        }

        private static string ValidateRequest(TorobRequestDto request)
        {
            if (request == null)
            {
                return "request body is required";
            }

            if (request.ExtraFields?.Count > 0)
            {
                return
                    $"unsupported parameter: {request.ExtraFields.Keys.First()}";
            }

            var hasPageMode =
                request.Page.HasValue ||
                !string.IsNullOrWhiteSpace(request.Sort);

            var hasUrlMode = request.PageUrls != null;
            var hasUniqueMode = request.PageUniques != null;

            var selectedModes =
                (hasPageMode ? 1 : 0) +
                (hasUrlMode ? 1 : 0) +
                (hasUniqueMode ? 1 : 0);

            if (selectedModes == 0)
            {
                return
                    "one request mode must be provided";
            }

            if (selectedModes > 1)
            {
                return
                    "only one request mode can be provided";
            }

            if (hasPageMode)
            {
                if (!request.Page.HasValue)
                {
                    return "page parameter is not provided";
                }

                if (request.Page.Value < 1)
                {
                    return
                        "page parameter must be greater than or equal to 1";
                }

                if (string.IsNullOrWhiteSpace(request.Sort))
                {
                    return "sort parameter is not provided";
                }

                if (request.Sort != SortByDateAdded &&
                    request.Sort != SortByDateUpdated)
                {
                    return "sort parameter is invalid";
                }
            }

            if (hasUrlMode)
            {
                if (request.PageUrls.Count == 0)
                {
                    return
                        "page_urls must contain at least one item";
                }

                if (request.PageUrls.Any(string.IsNullOrWhiteSpace))
                {
                    return "page_urls contains an invalid value";
                }
            }

            if (hasUniqueMode)
            {
                if (request.PageUniques.Count == 0)
                {
                    return
                        "page_uniques must contain at least one item";
                }

                if (request.PageUniques.Any(string.IsNullOrWhiteSpace))
                {
                    return
                        "page_uniques contains an invalid value";
                }
            }

            return null;
        }

        private static string HtmlToPlainText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return null;
            }

            var value = Regex.Replace(
                html,
                @"<br\s*/?>",
                " ",
                RegexOptions.IgnoreCase
            );

            value = Regex.Replace(
                value,
                @"<[^>]+>",
                " "
            );

            value = WebUtility.HtmlDecode(value);

            value = Regex.Replace(
                value,
                @"\s+",
                " "
            );

            return value.Trim();
        }

        private static string ToTehranIso8601(DateTime date)
        {
            var unspecified = DateTime.SpecifyKind(
                date,
                DateTimeKind.Unspecified
            );

            var value = new DateTimeOffset(
                unspecified,
                TimeSpan.FromMinutes(210)
            );

            return value.ToString(
                "yyyy-MM-dd'T'HH:mm:sszzz",
                CultureInfo.InvariantCulture
            );
        }

        private static string Limit(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            value = value.Trim();

            return value.Length <= maxLength
                ? value
                : value[..maxLength];
        }

        private static string NullIfEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value;
        }
    }
}
