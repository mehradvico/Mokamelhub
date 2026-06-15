using Application.Common.Dto.LocationPoint;
using Application.Services.CategorySrv.Dto;
using Application.Services.Filing.PictureSrv.Dto;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using Application.Services.ProductSrvs.DiscountGroupSrv.Dto;
using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using System;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductSrv.Dto
{
    public class ProductVDto : SeoFieldLangDto
    {
        public string ProductLabel { get; set; }

        public string SecondName { get; set; }
        public int? SellLimitCount { get; set; }

        public long? CategoryId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public Nullable<long> PictureId { get; set; }
        public long StatusId { get; set; }
        public long TypeId { get; set; }
        public string CodeValue { get; set; }
        public long BasePrice { get; set; }
        public long Price { get; set; }
        public long WholeSalePrice { get; set; }
        public int DiscountPercent { get; set; }
        public long? DiscountGroupId { get; set; }
        public DateTime? DiscountEndDate { get; set; }

        public int SellCount { get; set; }
        public int VisitCount { get; set; }
        public double RateAvg { get; set; }
        public int RateCount { get; set; }
        public string HowToUse { get; set; }
        public string Composition { get; set; }
        public string NutritionalValue { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long? StoreId { get; set; }
        public string AdminDescription { get; set; }
        public bool Active { get; set; }
        public StoreMinVDto Store { get; set; }
        public PointDto Location { get; set; }
        public CategoryVDto Category { get; set; }
        public CodeDto Status { get; set; }
        public CodeDto Type { get; set; }
        public PictureVDto Picture { get; set; }
        public BrandVDto Brand { get; set; }
        public DiscountGroupVDto DiscountGroup { get; set; }

        public ICollection<ProductItemMinDto> ProductItems { get; set; }
    }
}
