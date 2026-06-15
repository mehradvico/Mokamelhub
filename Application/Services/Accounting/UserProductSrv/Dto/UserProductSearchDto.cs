using Application.Common.Dto.Result;
using Application.Services.Accounting.UserProductSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Accounting.UserProductSrv.Dto
{
    public class UserProductSearchDto : BaseSearchDto<UserProduct, UserProductVDto>, IUserProductSearchFields
    {
        public UserProductSearchDto(UserProductInputDto dto, IQueryable<UserProduct> list, IMapper mapper) : base(dto, list, mapper)
        {
            ProductId = dto.ProductId;
            UserId = dto.UserId;
            HasTicketTime = dto.HasTicketTime;
        }

        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public bool? HasTicketTime { get; set; }


    }
}
