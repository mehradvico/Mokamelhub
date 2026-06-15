using Application.Common.Dto.Result;
using Application.Services.Order.AddressSrv.iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Order.AddressSrv.Dto
{
    public class AddressSearchDto : BaseSearchDto<Address, AddressVDto>, IAddressSearchFields
    {
        public AddressSearchDto(AddressInputDto dto, IQueryable<Address> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.UserId = dto.UserId;
        }


        public long UserId { get; set; }
    }
}
