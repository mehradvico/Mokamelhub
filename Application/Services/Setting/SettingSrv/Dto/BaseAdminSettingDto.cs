namespace Application.Services.Setting.SettingSrv.Dto
{
    public class BaseAdminSettingDto
    {
        public string AdminMobiles { get; set; }
        public int CartExpireTime { get; set; }
        public string PaymentUrl { get; set; }
        public string ReturnToSiteUrl { get; set; }
        public string ReturnToOrderUrl { get; set; }
        public double CurrencyCoefficient { get; set; }
        public float BonusPercent { get; set; }
    }
}
