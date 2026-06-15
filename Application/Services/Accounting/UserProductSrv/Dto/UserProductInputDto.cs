using Application.Common.Dto.Input;
using Application.Services.Accounting.UserProductSrv.Iface;

namespace Application.Services.Accounting.UserProductSrv.Dto
{
    public class UserProductInputDto : BaseInputDto, IUserProductSearchFields
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? HasTicketTime { get; set; }


    }
}
