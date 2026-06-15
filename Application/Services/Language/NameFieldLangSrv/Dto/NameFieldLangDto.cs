using Application.Common.Dto.Field;
using Application.Services.Language.LanguageSrv.Dto;

namespace Application.Services.Language.NameFieldLangSrv.Dto
{
    public class NameFieldLangDto : Name_FieldDto
    {
        public long LanguageId { get; set; }
        public LanguageDto LanguageDto { get; set; }
    }
}
