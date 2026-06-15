using System;
using System.Security.Cryptography;
using System.Text;


namespace Application.Common.Helpers
{
    public static class SecurityHelper
    {
        public static string Tosha256Hash(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            using var algorithm = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = algorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

    }
}
