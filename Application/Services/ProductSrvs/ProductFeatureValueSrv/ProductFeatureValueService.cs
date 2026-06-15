using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductFeatureValueSrv
{
    public class ProductFeatureValueService : IProductFeatureValueService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public ProductFeatureValueService(IDataBaseContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        public ProductFeatureValueSearchDto Search(ProductFeatureValueInputDto searchDto)
        {
            var query = _context.ProductFeatureValues.Include(s => s.Feature).ThenInclude(s => s.Type).Include(s => s.FeatureItem).Where(s => s.ProductId == searchDto.ProductId);

            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(searchDto.Q));
            }
            if (searchDto.Hide.HasValue)
            {
                query = query.Where(s => s.Feature.Hide == searchDto.Hide);
            }
            if (searchDto.FeatureTypeEnum.HasValue)
            {
                query = query.Where(s => s.Feature.Type.Label == searchDto.FeatureTypeEnum.ToString());
            }
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
                            query = query.OrderBy(s => s.Feature.Priority);

                            break;
                        }
                    case Common.Enumerable.SortEnum.New:
                        {
                            query = query.OrderByDescending(s => s.Feature.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Old:
                        {
                            query = query.OrderBy(s => s.Feature.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Name:
                        {
                            query = query.OrderByDescending(s => s.Feature.Name);
                            break;
                        }

                    case Common.Enumerable.SortEnum.MorePriority:
                        {
                            query = query.OrderBy(s => s.Feature.Priority);
                            break;
                        }
                    case Common.Enumerable.SortEnum.LessPriority:
                        {
                            query = query.OrderByDescending(s => s.Feature.Priority);
                            break;
                        }
                    default:
                        break;
                }
            }
            return new ProductFeatureValueSearchDto(searchDto, query.GroupBy(g => g.Feature), mapper);
        }
        public BaseResultDto GetForProduct(long productId)
        {
            var query = _context.ProductFeatureValues.Include(s => s.FeatureItem).Where(s => s.ProductId == productId);
            var result = new ProductFeatureValueAddDto();
            result.ProductId = productId;
            result.ProductFeatures = mapper.Map<List<ProductFeatureValueDto>>(query);
            return new BaseResultDto<ProductFeatureValueAddDto>(true, result);
        }
        public BaseResultDto SetForProduct(ProductFeatureValueAddDto productFeatureValues)
        {
            try
            {
                var featureIds = productFeatureValues.ProductFeatures.Select(s => s.FeatureId).ToList();
                var featureItemIds = productFeatureValues.ProductFeatures.Where(f => f.FeatureItemId.HasValue).Select(s => s.FeatureItemId).ToList();


                var existList = _context.ProductFeatureValues.AsTracking().Where(s => s.ProductId == productFeatureValues.ProductId);
                var deleteList = existList.Where(s => !featureIds.Contains(s.FeatureId) || (s.FeatureItemId.HasValue && !featureItemIds.Contains(s.FeatureItemId))).ToList();
                _context.ProductFeatureValues.RemoveRange(deleteList);
                foreach (var item in productFeatureValues.ProductFeatures)
                {
                    if (item.FeatureItemId == null)
                    {
                        var existItem = existList.FirstOrDefault(s => s.FeatureId == item.FeatureId);
                        if (existItem != null)
                        {
                            if (string.IsNullOrEmpty(item.Name))
                            {
                                _context.ProductFeatureValues.Remove(existItem);
                            }
                            else
                            {
                                existItem.Name = item.Name;
                                _context.ProductFeatureValues.Update(existItem);
                            }

                            continue;
                        }
                    }
                    else if (item.FeatureItemId.HasValue && existList.Any(a => a.FeatureItemId == item.FeatureItemId))
                    {
                        continue;
                    }
                    _context.ProductFeatureValues.Add(mapper.Map<ProductFeatureValue>(item));

                }
                _context.SaveChanges();
                return new BaseResultDto(true);
            }
            catch
            {
                return new BaseResultDto(false, val: Resource.Notification.Unsuccess);

            }

        }

    }
}
