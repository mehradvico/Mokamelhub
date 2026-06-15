using Application.Common.Dto.Field;
using Application.Services.Language.LanguageSrv.Dto;

namespace Application.Services.Language.FullNameFieldLangSrv.Dto
{
    public class FullNameFieldLangDto : FullName_FieldDto
    {
        public long LanguageId { get; set; }
        public LanguageDto LanguageDto { get; set; }
    }
}
