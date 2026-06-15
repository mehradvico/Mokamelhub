using Application.Common.Dto.Input;
using Application.Services.Order.AddressSrv.iface;

namespace Application.Services.Order.AddressSrv.Dto
{
    public class AddressInputDto : BaseInputDto, IAddressSearchFields
    {
        public long UserId { get; set; }
    }
}
