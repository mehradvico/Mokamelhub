using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class ConvertHelper
    {
        public static async Task<string> ToEnglishDigitsAsync(this string input)
        {

            await Task.Run(() =>
            {
                input = ToEnglishDigits(input);
            });
            return input;
        }
        public static string ToEnglishDigits(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3').Replace('۴', '4').Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');
            }
            return input;
        }
        public static string ToStandardUrl(this string url)
        {
            if (string.IsNullOrEmpty(url) == true)
                return url;
            url = url.Trim();
            url = url.Replace(" ", "-");
            url = url.Replace("%", "-");
            url = url.Replace(".", "-");
            url = url.Replace("&", "-");
            url = url.Replace("+", "-");
            url = url.Replace("?", "-");
            url = url.Replace("؟", "-");
            url = url.Replace("*", "-");
            url = url.Replace("/", "-");
            url = url.Replace("'", "-");
            url = url.Replace("(", "-");
            url = url.Replace(")", "-");
            url = url.Replace("|", "-");

            return url.ToLower();
        }
        public static string ToCurency(this double price)
        {
            return string.Format("{0} {1}", price.ToString("n0"), Resource.Lang.Currency);

        }

    }
}
