using System;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static async Task<string> ToShortDateAsync(this DateTime input)
        {
            string result = "";
            await Task.Run(() =>
            {
                result = ToShortDate(input);
            });
            return result;
        }
        public static string ToShortDate(this DateTime? input)
        {
            string result = "";

            if (input != null)
            {
                result = input.ToShortDate();
            }
            return result;

        }
        public static string ToShortDate(this DateTime input)
        {
            return input.ToString("yyyy/MM/dd");
        }
        //public static string ToPersian(this DateTime datetime)
        //{
        //    if (datetime != default(DateTime))
        //        return Number.ConvertToLatin(Calendar.ConvertToPersian(datetime).Simple.ToString());
        //    return "";
        //}
    }
}
