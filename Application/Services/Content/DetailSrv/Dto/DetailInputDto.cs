using Application.Common.Dto.Input;
using Application.Services.Content.DetailSrv.Iface;

namespace Application.Services.Content.DetailSrv.Dto
{
    public class DetailInputDto : BaseInputDto, IDetailSearchFields
    {
        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }
        public string Label { get; set; }
    }
}
