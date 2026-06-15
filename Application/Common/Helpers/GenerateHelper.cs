using System;

namespace Application.Common.Helpers
{
    public static class GenerateHelper
    {
        public static string RandomDigit(int length = 4)
        {
            string random = "";
            Random _rdm = new Random();
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                    random += _rdm.Next(1, 9).ToString();
                else
                    random += _rdm.Next(0, 9).ToString();

            }
            return random;
        }
    }
}
