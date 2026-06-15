using Application.Common.Dto.Field;

namespace Application.Services.Setting.SettingSrv.Dto
{
    public class AdminSettingDto : Name_FieldDto
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
