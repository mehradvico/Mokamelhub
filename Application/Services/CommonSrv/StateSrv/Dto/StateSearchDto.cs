using Application.Common.Dto.Result;
using Application.Services.CommonSrv.StateSrv.Iface;
using Application.Services.Language.BrandLangSrv.Dto;
using Application.Services.Language.BrandLangSrv.Iface;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommonSrv.StateSrv.Dto
{
    public class StateSearchDto : BaseSearchDto<State, StateVDto>, IStateSerchFields
    {
        public StateSearchDto(StateInputDto dto, IQueryable<State> list, IMapper mapper) : base(dto, list, mapper)
        {
            CountryId = dto.CountryId;
        }
        public long? CountryId { get; set; }
    }
}
