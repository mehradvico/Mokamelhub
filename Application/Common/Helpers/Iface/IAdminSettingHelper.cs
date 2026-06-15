using Application.Services.Setting.SettingSrv.Dto;

namespace Application.Common.Helpers.Iface
{
    public interface IAdminSettingHelper
    {

        public BaseAdminSettingDto BaseAdminSetting { get; }
        public CargoPriceDto CargoPrice { get; }
        public SharePriceDto SharePrice { get; }
    }
}
