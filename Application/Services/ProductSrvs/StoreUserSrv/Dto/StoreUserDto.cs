namespace Application.Services.ProductSrvs.StoreUserSrv.Dto
{
    public class StoreUserDto
    {
        public long StoreId { get; set; }
        public long UserId { get; set; }
        public bool? Active { get; set; }
    }
}
