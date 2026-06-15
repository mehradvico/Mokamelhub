namespace Application.Services.Order.MerchantSrv.Dto.SamanKishDto
{
    public class SamanKishRequestDto
    {

        public string Action { get; set; } = "token";
        public string TerminalId { get; set; }
        public double Amount { get; set; }
        public string ResNum { get; set; }
        public string RedirectUrl { get; set; }
        public string CellNumber { get; set; }
    }
}
