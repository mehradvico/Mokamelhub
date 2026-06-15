using Application.Common.Dto.Field;
using Application.Services.CategorySrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;

namespace Application.Services.Content.DetailSrv.Dto
{
    public class DetailVDto : Id_FieldDto
    {
        public string Label { get; set; }
        public string UiLabel { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public long TypeId { get; set; }
        public long CategoryId { get; set; }
        public CodeDto Type { get; set; }
        public CategoryVDto Category { get; set; }

    }
}
