using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SupplyFlow.Helpers
{
    public class PasswordHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32;  // 256 bit
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
                var hash = pbkdf2.GetBytes(KeySize);

                var result = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, result, 0, SaltSize);
                Array.Copy(hash, 0, result, SaltSize, KeySize);

                return Convert.ToBase64String(result);
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var bytes = Convert.FromBase64String(storedHash);

            var salt = new byte[SaltSize];
            Array.Copy(bytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++)
            {
                if (bytes[i + SaltSize] != hash[i])
                    return false;
            }
            return true;
        }
    }
}