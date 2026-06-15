using Application.Common.Dto.Result;
using Application.Services.Language.PostLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.PostLangSrv.Dto
{
    public class PostLangSearchDto : BaseSearchDto<SeoFieldLang, SeoFieldLangDto>, IPostLangSearchFields
    {
        public PostLangSearchDto(PostLangInputDto dto, IQueryable<SeoFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            PostId = dto.PostId;
        }

        public long PostId { get; set; }
    }
}
