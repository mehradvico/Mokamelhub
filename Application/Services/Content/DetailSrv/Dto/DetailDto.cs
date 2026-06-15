using Application.Common.Dto.Field;

namespace Application.Services.Content.DetailSrv.Dto
{
    public class DetailDto : Id_FieldDto
    {
        public string Label { get; set; }
        public string UiLabel { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public long TypeId { get; set; }
        public long CategoryId { get; set; }


    }
}

