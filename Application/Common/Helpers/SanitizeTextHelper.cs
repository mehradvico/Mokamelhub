using Ganss.Xss;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class SanitizeTextHelper
    {
        public static async Task<string> ToSanitizeAsync(this string input)
        {

            await Task.Run(() =>
            {
                input = ToSanitizeText(input);
            });
            return input;
        }

        public static string ToSanitizeText(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var sanitize = new HtmlSanitizer();
                input = sanitize.Sanitize(input);
            }
            return input;
        }
    }
}
