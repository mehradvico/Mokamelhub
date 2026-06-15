namespace Application.Services.Accounting.UserProductSrv.Iface
{
    public interface IUserProductSearchFields
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? HasTicketTime { get; set; }


    }
}
