using Entities.Entities;
using Entities.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Interface
{
    public interface IDataBaseContext : IDbContextTransactionManager
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<CodeGroup> CodeGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureItem> FeatureItems { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductFeatureValue> ProductFeatureValues { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<PostPicture> PostPictures { get; set; }
        public DbSet<SeoFieldLang> SeoFieldLangs { get; set; }
        public DbSet<NameFieldLang> NameFieldLangs { get; set; }
        public DbSet<Variety> Varieties { get; set; }
        public DbSet<VarietyItem> VarietyItems { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<FullNameFieldLang> FullNameFieldLangs { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<OtpVerify> OtpVerifies { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<Sms> Smses { get; set; }
        public DbSet<SmsNumber> SmsNumbers { get; set; }
        public DbSet<SmsProvider> SmsProviders { get; set; }
        public DbSet<SmsSetting> SmsSettings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Rebate> Rebate { get; set; }
        public DbSet<ProductOrderItem> ProductOrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<ContactUsGroup> ContactUsGroups { get; set; }
        public DbSet<ProductRelate> ProductRelates { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryDistance> DeliveryDistances { get; set; }
        public DbSet<BaseDetail> BaseDetails { get; set; }
        public DbSet<AdminSetting> AdminSettings { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreComment> StoreComments { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<EmailHost> EmailHosts { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<CartStore> CartStores { get; set; }
        public DbSet<ProductOrderStore> ProductOrderStores { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountGroup> DiscountGroups { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<ProductReport> ProductReports { get; set; }
        public DbSet<ContactUsItem> ContactUsItems { get; set; }
        public DbSet<Country> Countries { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
        void Dispose();
    }
}
