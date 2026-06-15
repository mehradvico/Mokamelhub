namespace Application.Services.Setting.SmsSrv.Dto
{
    public class KavenegarResultDto
    {
        public Return @return { get; set; }
        public Entries entries { get; set; }
    }
    public class Return
    {
        public int status { get; set; }
        public string message { get; set; }
    }
    public class Entries
    {
        public int messageid { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string statustext { get; set; }
        public string sender { get; set; }
        public string receptor { get; set; }
        public long date { get; set; }
        public int cost { get; set; }
    }

}
