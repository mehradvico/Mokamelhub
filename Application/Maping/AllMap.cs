using Application.Common.Dto.LocationPoint;
using Application.Common.Enumerable.Code;
using Application.Common.Helpers;
using Application.Services.Accounting.PermissionSrv.Dto;
using Application.Services.Accounting.RolePermission.Dto;
using Application.Services.Accounting.TicketItemSrv.Dto;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.UserProductSrv.Dto;
using Application.Services.Accounting.UserTokenSrv.Dto;
using Application.Services.CategorySrv.Dto;
using Application.Services.CommonSrv.CitySrv.Dto;
using Application.Services.CommonSrv.CommentLikeSrv.Dto;
using Application.Services.CommonSrv.CountrySrv.Dto;
using Application.Services.CommonSrv.NeighborhoodSrv.Dto;
using Application.Services.CommonSrv.StateSrv.Dto;
using Application.Services.Content.BannerSrv.Dto;
using Application.Services.Content.ContactUsGroupSrv.Dto;
using Application.Services.Content.ContactUsItemSrv.Dto;
using Application.Services.Content.ContactUsSrv.Dto;
using Application.Services.Content.DetailSrv.Dto;
using Application.Services.Content.GalleryItemSrv.Dto;
using Application.Services.Content.GallerySrv.Dto;
using Application.Services.Content.HashtagSrv.Dto;
using Application.Services.Content.NewsletterSrv.Dto;
using Application.Services.Content.PostCommentSrv.Dto;
using Application.Services.Content.PostFileSrv.Dto;
using Application.Services.Content.PostPictureSrv.Dto;
using Application.Services.Content.PostProductSrv.Dto;
using Application.Services.Content.PostSrv.Dto;
using Application.Services.Content.StaticPageSrv.Dto;
using Application.Services.Dto;
using Application.Services.Filing.FileSrv.Dto;
using Application.Services.Filing.PictureSrv.Dto;
using Application.Services.Language.FullNameFieldLangSrv.Dto;
using Application.Services.Language.LanguageSrv.Dto;
using Application.Services.Language.NameFieldLangSrv.Dto;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.Order.AddressSrv.Dto;
using Application.Services.Order.BankSrv.Dto;
using Application.Services.Order.CartItemSrv.Dto;
using Application.Services.Order.CartSrv.Dto;
using Application.Services.Order.CartStoreSrv.Dto;
using Application.Services.Order.DeliveryDistanceSrv.Dto;
using Application.Services.Order.DeliverySrv.Dto;
using Application.Services.Order.MerchantSrv.Dto;
using Application.Services.Order.ProductOrderItemSrv.Dto;
using Application.Services.Order.ProductOrderSrv.Dto;
using Application.Services.Order.ProductOrderStoreSrv.Dto;
using Application.Services.Order.RebateSrv.Dto;
using Application.Services.ProductSrvs.BrandCategorySrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using Application.Services.ProductSrvs.DiscountGroupSrv.Dto;
using Application.Services.ProductSrvs.DiscountSrv.Dto;
using Application.Services.ProductSrvs.FeatureItemSrv.Dto;
using Application.Services.ProductSrvs.FeatureSrv.Dto;
using Application.Services.ProductSrvs.ProductCommentSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto;
using Application.Services.ProductSrvs.ProductFileSrv.Dto;
using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using Application.Services.ProductSrvs.ProductLikeSrv.Dto;
using Application.Services.ProductSrvs.ProductPictureSrv.Dto;
using Application.Services.ProductSrvs.ProductReportSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.ProductSrvs.StoreCommentSrv.Dto;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using Application.Services.ProductSrvs.WalletSrv.Dto;
using Application.Services.Setting.BaseDetailSrv.Dto;
using Application.Services.Setting.CodeGroupSrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using AutoMapper;
using Entities.Entities;
using Entities.Entities.Security;
using NetTopologySuite.Geometries;
using Resource;
using System;
using System.Linq;

namespace Application.Maping
{
    public class AllMap : Profile
    {

        public AllMap()
        {
            //From A to Z


            //Address
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressVDto>().ReverseMap();
            //Address End ----------------------------------------------


            //Bank
            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<Bank, BankVDto>();
            //Bank End ----------------------------------------------


            //Banner
            CreateMap<Banner, BannerDto>().ReverseMap();
            CreateMap<Banner, BannerVDto>();
            //Banner End ----------------------------------------------


            //Brand
            CreateMap<Brand, BrandVDto>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>().ForMember(x => x.Picture, y => y.Ignore());
            CreateMap<Brand, BrandCategoryDto>().ForMember(x => x.BrandDto, y => y.MapFrom(y => y)).ForMember(x => x.CategoryDto, y => y.MapFrom(y => y.Categories));
            //Brand End ----------------------------------------------


            //Cart
            CreateMap<Cart, CartVDto>().ReverseMap();
            CreateMap<CartItem, CartItemVDto>().ReverseMap();
            CreateMap<Cart, ProductOrderDto>().ForMember(x => x.DiscountPrice, o => o.MapFrom(m => m.BasePrice - m.Price)).ForMember(x => x.ProductOrderStores, o => o.MapFrom(m => m.CartStores.Where(s => s.Active))).ForMember(x => x.DeliveryTypeId, o => o.MapFrom(m => m.Delivery.DeliveryTypeId));
            CreateMap<CartStore, ProductOrderStoreDto>().ForMember(x => x.DiscountPrice, o => o.MapFrom(m => m.BasePrice - m.Price)).ForMember(x => x.ProductOrderItems, o => o.MapFrom(m => m.CartItems)).ForMember(x => x.Id, y => y.Ignore());
            CreateMap<CartStore, ProductOrderDto>();
            CreateMap<CartStore, CartStoreDto>().ReverseMap();
            CreateMap<CartStore, CartStoreVDto>();
            CreateMap<CartItem, ProductOrderItemDto>().ForMember(x => x.DiscountPrice, o => o.MapFrom(m => m.ProductItem.BasePrice - m.ProductItem.Price)).ForMember(x => x.DiscountPercent, o => o.MapFrom(m => m.ProductItem.DiscountPercent)).ForMember(x => x.BasePrice, o => o.MapFrom(m => m.ProductItem.BasePrice)).ForMember(x => x.Price, o => o.MapFrom(m => m.ProductItem.Price)).ForMember(x => x.Id, y => y.Ignore());
            //Cart End ----------------------------------------------


            //Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ForMember(x => x.Picture, y => y.Ignore()).ForMember(x => x.Icon, y => y.Ignore());
            CreateMap<Category, CategoryVDto>();
            CreateMap<Category, CategoryParentVDto>();
            CreateMap<Category, CategoryChildrenVDto>();
            CreateMap<Category, CategoryCompleteVDto>();
            CreateMap<Category, SearchCategoryDto>();
            //Category End ----------------------------------------------


            //Code
            CreateMap<CodeGroup, CodeGroupDto>().ReverseMap();
            CreateMap<Code, CodeDto>().ReverseMap();
            CreateMap<Code, CodeVDto>();
            //Code End ----------------------------------------------


            //ContactUs
            CreateMap<ContactUs, ContactUsDto>().ReverseMap();
            CreateMap<ContactUs, ContactUsVDto>().ReverseMap();
            CreateMap<ContactUsItem, ContactUsItemDto>().ReverseMap();
            CreateMap<ContactUsGroup, ContactUsGroupDto>().ReverseMap();
            CreateMap<ContactUsItem, ContactUsItemDto>().ReverseMap();
            //ContactUs End ----------------------------------------------


            //Comment
            CreateMap<CommentLike, CommentLikeDto>().ReverseMap();
            //Comment End ----------------------------------------------


            //City
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityVDto>();
            CreateMap<City, CityStateVDto>().ForMember(x => x.StateName, o => o.MapFrom(m => m.State.Name)).AfterMap((src, dest) => dest.FullName = $"{dest.StateName} - {dest.Name}");
            //City End ----------------------------------------------


            //Country
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CountryVDto>();
            //Country End ----------------------------------------------


            //Delivery
            CreateMap<Delivery, DeliveryDto>().ReverseMap();
            CreateMap<Delivery, DeliveryVDto>();
            CreateMap<Delivery, DeliveryResultVDto>();
            CreateMap<DeliveryDistance, DeliveryDistanceVDto>();
            CreateMap<DeliveryDistance, DeliveryDistanceDto>().ReverseMap();
            //Delivery End ----------------------------------------------


            //Detail
            CreateMap<Detail, DetailDto>().ReverseMap();
            CreateMap<Detail, DetailVDto>().ReverseMap();
            CreateMap<BaseDetail, BaseDetailDto>().ReverseMap();
            //Detail End ----------------------------------------------


            //Discount
            CreateMap<Discount, DiscountVDto>();
            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountDto, Discount>().ForMember(x => x.Synced, y => y.Ignore());
            CreateMap<DiscountGroup, DiscountGroupVDto>();
            CreateMap<DiscountGroup, DiscountGroupDto>();
            CreateMap<DiscountGroupDto, DiscountGroup>().ForMember(x => x.Picture, y => y.Ignore());
            //Discount End ----------------------------------------------


            //Feature
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Feature, FeatureVDto>().ReverseMap();
            CreateMap<Feature, FeatureMinVDto>();
            CreateMap<FeatureItem, FeatureItemDto>().ReverseMap();
            CreateMap<FeatureItem, FeatureItemVDto>();
            CreateMap<ProductFeatureValue, ProductFeatureValueDto>().ReverseMap();
            CreateMap<ProductFeatureValue, ProductFeatureValueVDto>();
            CreateMap<ProductFeatureValue, ProductFeatureValueMinVDto>();
            CreateMap<Category, CategoryFeatureDto>().ReverseMap();
            CreateMap<Feature, ProductFeatureVDto>();
            CreateMap<IGrouping<Feature, ProductFeatureValue>, ProductFeatureVDto>().ForMember(x => x.ProductFeatureValues, o => o.MapFrom(m => m.ToList()));
            //Feature End ----------------------------------------------


            //File
            CreateMap<File, FileDto>().ReverseMap();
            CreateMap<File, FileVDto>().ForMember(x => x.Url, y => y.MapFrom(w => w.Url + "/" + w.Name));
            //File End ----------------------------------------------


            //Gallery
            CreateMap<Gallery, GalleryVDto>();
            CreateMap<Gallery, GalleryDto>().ReverseMap();
            CreateMap<GalleryItem, GalleryItemDto>().ReverseMap();
            CreateMap<GalleryItem, GalleryItemVDto>();
            //Gallery End ----------------------------------------------


            //Hashtag
            CreateMap<Hashtag, HashtagDto>();
            //Hashtag End ----------------------------------------------


            //Language
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<SeoFieldLang, SeoFieldLangDto>().ForMember(x => x.LanguageDto, y => y.MapFrom(s => s.Language));
            CreateMap<SeoFieldLangDto, SeoFieldLang>();
            CreateMap<NameFieldLang, NameFieldLangDto>().ForMember(x => x.LanguageDto, y => y.MapFrom(s => s.Language));
            CreateMap<NameFieldLangDto, NameFieldLang>();
            CreateMap<FullNameFieldLang, FullNameFieldLangDto>().ForMember(x => x.LanguageDto, y => y.MapFrom(s => s.Language));
            CreateMap<FullNameFieldLangDto, FullNameFieldLang>();
            //Language End ----------------------------------------------


            //Merchant 
            CreateMap<Merchant, MerchantDto>().ReverseMap();
            CreateMap<Merchant, MerchantVDto>().ReverseMap();
            //Merchant End ----------------------------------------------


            //Neighborhood
            CreateMap<Neighborhood, NeighborhoodDto>().ReverseMap();
            CreateMap<Neighborhood, NeighborhoodVDto>();
            //Neighborhood End ----------------------------------------------


            //NewsLetter
            CreateMap<Newsletter, NewsletterDto>().ReverseMap();
            //NewsLetter ----------------------------------------------


            //Permission
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionVDto>();
            CreateMap<Permission, PermissionMenuDto>();
            CreateMap<Role, RolePermissionDto>().ForMember(x => x.RoleDto, y => y.MapFrom(y => y)).ForMember(x => x.PermissionsDto, y => y.MapFrom(y => y.Permissions));
            //Permission End ----------------------------------------------


            //Picture
            CreateMap<Picture, PictureVDto>().ForMember(x => x.BaseUrl, o => o.MapFrom(m => m.Url)).ForMember(x => x.Url, o => o.MapFrom(m => m.Url + "/" + m.Name));
            CreateMap<Picture, PictureDto>().ReverseMap();
            CreateMap<PictureVDto, Picture>().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            //Picture End ----------------------------------------------


            //Post
            CreateMap<PostPicture, PostPictureVDto>();
            CreateMap<PostFile, PostFileVDto>();
            CreateMap<Post, PostVDto>().ForMember(x => x.PostPictures, o => o.MapFrom(m => m.PostPictures)).ForMember(x => x.PostFiles, o => o.MapFrom(m => m.PostFiles))/*.ForMember(x => x.CreateDate, o => o.MapFrom(m => m.CreateDate.ToShortDate())).ForMember(x => x.PublishDate, o => o.MapFrom(m => m.PublishDate.ToShortDate()))*/;
            CreateMap<Post, PostDto>().ForMember(x => x.PostPicturesList, o => o.MapFrom(m => m.PostPictures)).ForMember(x => x.PostFilesList, o => o.MapFrom(m => m.PostFiles)).ForMember(x => x.CategoryIds, o => o.MapFrom(m => m.Categories.Select(s => s.Id))).ForMember(x => x.HashTagList, o => o.MapFrom(m => m.Hashtags.Select(s => s.Name)));
            CreateMap<PostDto, Post>().ForMember(x => x.CommentCount, y => y.Ignore()).ForMember(x => x.CreateDate, y => y.Ignore()).ForMember(x => x.VisitCount, y => y.Ignore()).ForMember(x => x.Categories, y => y.Ignore()).ForMember(x => x.PostFiles, y => y.Ignore()).ForMember(x => x.Hashtags, y => y.Ignore()).ForMember(x => x.Picture, y => y.Ignore());
            CreateMap<PostComment, PostCommentVDto>().ForMember(x => x.CreateDate, o => o.MapFrom(m => m.CreateDate.ToShortDate())).ForMember(x => x.PostName, o => o.MapFrom(m => m.Post.Name));
            CreateMap<PostComment, PostCommentDto>().ReverseMap();
            CreateMap<PostFileDto, PostFile>().ForMember(x => x.Id, opt => opt.Ignore()).ForMember(x => x.File, opt => opt.Ignore());
            CreateMap<PostFile, PostFileDto>();
            CreateMap<PostPictureDto, PostPicture>().ForMember(x => x.Id, opt => opt.Ignore()).ForMember(x => x.Picture, opt => opt.Ignore());
            CreateMap<PostPicture, PostPictureDto>();
            CreateMap<Post, PostProductDto>().ForMember(s => s.Products, o => o.MapFrom(m => m.Products));
            //Post End ----------------------------------------------


            //Product
            CreateMap<Product, ProductDto>().ForMember(x => x.CategoryIds, o => o.MapFrom(m => m.Categories.Select(s => s.Id)));
            CreateMap<Product, ProductVDto>();
            CreateMap<Product, ProductGroupVDto>();
            CreateMap<Product, ProductMinVDto>();
            CreateMap<ProductDto, Product>().ForMember(x => x.Variety, y => y.Ignore()).ForMember(x => x.Variety2, y => y.Ignore()).ForMember(x => x.Picture, y => y.Ignore()).ForMember(x => x.CreateDate, y => y.Ignore()).ForMember(x => x.ProductPictures, y => y.Ignore());
            CreateMap<ProductDuplicateDto, Product>();
            CreateMap<ProductDuplicateDto, ProductDto>();
            CreateMap<Product, ProductItemForProductDto>().ForMember(x => x.Variety1, o => o.MapFrom(m => m.Variety));
            CreateMap<ProductItemForProductVDto, ProductItemForProductDto>();
            CreateMap<ProductPicture, ProductPictureVDto>();
            CreateMap<ProductPicture, ProductPictureDto>();
            CreateMap<ProductPictureDto, ProductPicture>().ForMember(x => x.Picture, y => y.Ignore());
            CreateMap<ProductFile, ProductFileDto>().ReverseMap();
            CreateMap<ProductFile, ProductFileVDto>();
            CreateMap<ProductComment, ProductCommentDto>().ReverseMap();
            CreateMap<ProductComment, ProductCommentVDto>().ForMember(x => x.UserIsLike, o => o.MapFrom(m => (m.CommentLikes != null && m.CommentLikes.Any()) ? (bool?)m.CommentLikes.First().IsLike : null)).ForMember(x => x.ProductName, o => o.MapFrom(m => m.Product.Name)); ;
            CreateMap<ProductLike, ProductLikeDto>().ReverseMap();
            CreateMap<ProductItemDto, ProductItem>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasePrice)).ForMember(dest => dest.WholeSalePrice, opt => opt.MapFrom(src => src.WholeSalePrice)).ForMember(dest => dest.VarietyItem, opt => opt.Ignore()).ForMember(dest => dest.VarietyItem2, opt => opt.Ignore()).ForMember(dest => dest.Store, opt => opt.Ignore());
            CreateMap<ProductItem, ProductItemDto>().ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.Price)).ForMember(dest => dest.WholeSalePrice, opt => opt.MapFrom(src => src.WholeSalePrice));
            CreateMap<ProductItem, ProductItemVDto>().ForMember(dest => dest.VarietyName, opt => opt.MapFrom(src => src.VarietyItem.Name));
            CreateMap<ProductItem, ProductItemFullVDto>();
            CreateMap<ProductItem, ProductItemShowVDto>();
            CreateMap<ProductItem, ProductItemForProductVDto>();
            CreateMap<ProductItem, ProductItemMinDto>();
            CreateMap<ProductItemDto, ProductItemListUpdateDto>();
            CreateMap<Product, ProductItemForProductDto>().ForMember(x => x.Variety1, o => o.MapFrom(m => m.Variety));
            CreateMap<ProductItemForProductVDto, ProductItemForProductDto>();
            CreateMap<UserProduct, UserProductDto>().ReverseMap();
            CreateMap<UserProduct, UserProductVDto>();
            CreateMap<ProductReport, ProductReportDto>().ReverseMap();
            CreateMap<ProductReport, ProductReportVDto>();
            //Product End ----------------------------------------------


            //ProductOrder
            CreateMap<ProductOrder, ProductOrderDto>().ReverseMap();
            CreateMap<ProductOrder, ProductOrderVDto>();
            CreateMap<ProductOrderStore, ProductOrderStoreDto>().ReverseMap();
            CreateMap<ProductOrderStore, ProductOrderStoreVDto>();
            CreateMap<ProductOrderItem, ProductOrderItemDto>().ReverseMap();
            CreateMap<ProductOrderItem, ProductOrderItemVDto>();
            //ProductOrder End ----------------------------------------------


            //Payment
            CreateMap<PaymentDto, Payment>().ForMember(x => x.User, y => y.Ignore()).ForMember(x => x.Merchant, y => y.Ignore()).ForMember(x => x.Type, y => y.Ignore());
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentVDto>();
            CreateMap<PaymentStartDto, Payment>().ForMember(x => x.User, y => y.Ignore()).ForMember(x => x.Merchant, y => y.Ignore());
            //Payment End ----------------------------------------------


            //Point
            CreateMap<Point, PointDto>();
            CreateMap<PointDto, Point>().ForMember(s => s.SRID, o => o.MapFrom(m => 4326));
            //Point End ----------------------------------------------


            //Rebate 
            CreateMap<RebateDto, Rebate>().ForMember(x => x.UsedCount, y => y.Ignore());
            CreateMap<Rebate, RebateDto>();
            CreateMap<Rebate, RebateVDto>();
            //Rebate End ----------------------------------------------


            //State
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<State, StateVDto>();
            //State End ----------------------------------------------


            //StaticPage
            CreateMap<StaticPage, StaticPageDto>().ReverseMap();
            CreateMap<StaticPage, StaticPageVDto>();
            //StaticPage ----------------------------------------------


            //Store
            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>().ForMember(x => x.Users, opt => opt.Ignore()).ForMember(x => x.RateAvg, y => y.Ignore()).ForMember(x => x.RateCount, y => y.Ignore()).ForMember(x => x.CommentCount, y => y.Ignore()).ForMember(x => x.MaxDiscountPercent, y => y.Ignore()).ForMember(x => x.Picture, y => y.Ignore()).ForMember(x => x.Icon, y => y.Ignore());
            CreateMap<Store, StoreMinVDto>().ForMember(x => x.StoreId, o => o.MapFrom(m => m.Id));
            CreateMap<Store, StoreVDto>();
            CreateMap<Store, StoreFinanceVDto>().ForMember(d => d.HasCommission, o => o.MapFrom(s => s.CommissionPercent != 0));
            CreateMap<StoreComment, StoreCommentVDto>().ForMember(x => x.CreateDate, o => o.MapFrom(m => m.CreateDate.ToShortDate())).ForMember(x => x.StoreName, o => o.MapFrom(m => m.Store.Name));
            CreateMap<StoreComment, StoreCommentDto>().ReverseMap();
            CreateMap<Store, SearchStoreDto>().ForMember(d => d.Icon, o => o.MapFrom(s => s.Icon)).ForMember(d => d.Picture, o => o.MapFrom(s => s.Picture));
            //Store End ----------------------------------------------


            //Ticket
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Ticket, TicketVDto>().ReverseMap();
            CreateMap<TicketItem, TicketItemDto>().ReverseMap();
            CreateMap<TicketItem, TicketItemVDto>().ReverseMap();
            //Ticket End ----------------------------------------------


            //User
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.Picture, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.Ignore())
                .ForMember(x => x.CreateDate, opt => opt.Ignore())
                .ForMember(x => x.BonusCode, opt => opt.Ignore())
                .ForMember(x => x.RequestCode, opt => opt.Ignore())
                .ForMember(x => x.RequestCodeTryCount, opt => opt.Ignore())
                .ForMember(x => x.Deleted, opt => opt.Ignore())
                .ForMember(x => x.UserTokens, opt => opt.Ignore())
                .ForMember(x => x.Carts, opt => opt.Ignore())
                .ForMember(x => x.CartItems, opt => opt.Ignore())
                .ForMember(x => x.ProductOrders, opt => opt.Ignore())
                .ForMember(x => x.Rebates, opt => opt.Ignore())
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.UserProducts, opt => opt.Ignore())
                .ForMember(x => x.ProductLikes, opt => opt.Ignore())
                .ForMember(x => x.Stores, opt => opt.Ignore());
            CreateMap<User, UserVDto>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(m => $"{m.FirstName ?? ""} {m.LastName ?? ""}".Trim()))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(m => m.Role != null ? m.Role.Name : null));
            CreateMap<User, UserMinVDto>().ForMember(x => x.FullName, opt => opt.MapFrom(m => $"{m.FirstName ?? ""} {m.LastName ?? ""}".Trim()));
            CreateMap<User, CurrentUserDto>()
                .ForMember(x => x.RoleEnum, opt => opt.MapFrom(m => m.Role != null ? m.Role.Label : null))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(m => m.Role != null ? m.Role.Name : null))
                .ForMember(x => x.UserId, opt => opt.MapFrom(m => m.Id))
                .ForMember(x => x.IsFemale, opt => opt.MapFrom(m => m.IsFemale))
                .ForMember(x => x.FullName, opt => opt.MapFrom(m => $"{m.FirstName ?? ""} {m.LastName ?? ""}".Trim()))
                .ForMember(x => x.PictureId, opt => opt.MapFrom(m => m.PictureId));
            CreateMap<SignUpDto, UserDto>();
            CreateMap<CreateUserTokenDto, UserTokenDto>();
            CreateMap<CreateUserTokenDto, UserToken>();
            //User End ----------------------------------------------


            //Variety
            CreateMap<Variety, VarietyDto>().ReverseMap();
            CreateMap<Variety, VarietyVDto>();
            CreateMap<Variety, VarietyShowVDto>();
            CreateMap<VarietyItem, VarietyItemDto>().ReverseMap();
            CreateMap<VarietyItem, VarietyItemVDto>().ForMember(x => x.VarietyName, y => y.MapFrom(w => w.Variety.Name));
            CreateMap<VarietyItem, VarietyItemMinVDto>();
            CreateMap<IGrouping<VarietyItem, ProductItem>, VarietyItemShowVDto>().ForMember(x => x.VarietyItem, o => o.MapFrom(m => m.Key)).ForMember(x => x.ProductItems, o => o.MapFrom(m => m.Where(s => s.SystemActive).ToList()));
            //Variety End ----------------------------------------------


            //Wallet
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, WalletVDto>();
            CreateMap<Wallet, Wallet>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.ProductOrderId, y => y.Ignore()).ForMember(x => x.ProductOrder, y => y.Ignore());
            //Wallet End ----------------------------------------------
        }
    }
}
