using Application.Common.Dto.Field;
using Application.Services.Language.LanguageSrv.Dto;

namespace Application.Services.Language.SeoFieldLangSrv.Dto
{
    public class SeoFieldLangDto : Seo_FieldDto
    {
        public long LanguageId { get; set; }
        public LanguageDto LanguageDto { get; set; }

    }
}
