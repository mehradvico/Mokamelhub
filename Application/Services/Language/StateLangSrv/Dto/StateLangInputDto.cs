using Application.Common.Dto.Input;
using Application.Services.Language.StateLangSrv.Iface;

namespace Application.Services.Language.StateLangSrv.Dto
{
    public class StateLangInputDto : BaseInputDto, IStateLangSearchFields
    {
        public long StateId { get; set; }
    }
}
