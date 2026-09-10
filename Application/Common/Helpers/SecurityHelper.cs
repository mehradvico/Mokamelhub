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

        // Passwords must use a slow, salted hash (unlike Tosha256Hash above, which stays
        // in use for hashing high-entropy tokens where salting/slowness isn't needed).
        private const string Pbkdf2Prefix = "PBKDF2";
        private const int Pbkdf2Iterations = 210_000;
        private const int Pbkdf2SaltSize = 16;
        private const int Pbkdf2KeySize = 32;

        public static string ToPasswordHash(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return password;
            }

            var salt = RandomNumberGenerator.GetBytes(Pbkdf2SaltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Pbkdf2Iterations,
                HashAlgorithmName.SHA256,
                Pbkdf2KeySize);

            return string.Join(
                '.',
                Pbkdf2Prefix,
                Pbkdf2Iterations.ToString(),
                Convert.ToBase64String(salt),
                Convert.ToBase64String(key));
        }

        /// <summary>
        /// Verifies a plaintext password against a stored hash. Accepts both the new
        /// PBKDF2 format and the legacy unsalted Tosha256Hash format so existing accounts
        /// keep working; callers should re-hash with ToPasswordHash() after a successful
        /// legacy-format verification (see UserService.SignIn) to migrate them silently.
        /// </summary>
        public static bool VerifyPasswordHash(this string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
                return false;

            if (!storedHash.StartsWith(Pbkdf2Prefix + ".", StringComparison.Ordinal))
                return password.Tosha256Hash() == storedHash;

            var parts = storedHash.Split('.');
            if (parts.Length != 4 || !int.TryParse(parts[1], out var iterations))
                return false;

            byte[] salt, expectedKey;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedKey = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            var actualKey = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedKey.Length);

            return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
        }

        public static bool IsLegacyPasswordHash(this string storedHash) =>
            !string.IsNullOrEmpty(storedHash) && !storedHash.StartsWith(Pbkdf2Prefix + ".", StringComparison.Ordinal);
    }
}
