using System.Collections.Generic;

namespace Application.Services.Order.SnappPaySrv
{
    public class SnappPayOptions
    {
        public const string SectionName = "SnappPay";

        public bool Enabled { get; set; }
        public string BaseUrl { get; set; } = "https://fms-gateway-staging.apps.public.okd4.teh-1.snappcloud.io/";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CommissionType { get; set; } = 100;
        public int TimeoutSeconds { get; set; } = 30;
        public List<string> PaymentMethodTypes { get; set; } = new();
    }
}
