using Application.Common.Enumerable;
using Application.Common.Helpers.Iface;
using Application.Services.Setting.SettingSrv;
using Application.Services.Setting.SettingSrv.Dto;
using Newtonsoft.Json;

namespace Application.Common.Helpers
{
    public class AdminSettingHelper : IAdminSettingHelper
    {
        private readonly IAdminSettingService _adminSettingService;

        public AdminSettingHelper(IAdminSettingService adminSettingService)
        {
            _adminSettingService = adminSettingService;
        }
        private BaseAdminSettingDto BaseAdminSettingDto()
        {
            var item = _adminSettingService.GetByLabel(AdminSettingEnum.BaseAdminSetting.ToString());
            if (item != null)
            {
                return JsonConvert.DeserializeObject<BaseAdminSettingDto>(item.Value);
            }
            return null;
        }
        public BaseAdminSettingDto BaseAdminSetting
        {
            get
            {
                return BaseAdminSettingDto();
            }
        }
        private CargoPriceDto CargoPriceDto()
        {
            var item = _adminSettingService.GetByLabel(AdminSettingEnum.CargoPrice.ToString());
            if (item != null)
            {
                return JsonConvert.DeserializeObject<CargoPriceDto>(item.Value);
            }
            return null;
        }
        public CargoPriceDto CargoPrice
        {
            get
            {
                return CargoPriceDto();
            }
        }

        private SharePriceDto SharePriceDto()
        {
            var item = _adminSettingService.GetByLabel(AdminSettingEnum.SharePrice.ToString());
            if (item != null)
            {
                return JsonConvert.DeserializeObject<SharePriceDto>(item.Value);
            }
            return null;
        }
        public SharePriceDto SharePrice
        {
            get
            {
                return SharePriceDto();
            }
        }
    }
}
