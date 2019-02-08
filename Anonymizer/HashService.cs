using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Anonymizer
{
    class HashService
    {
        private SHA512CryptoServiceProvider Service = new SHA512CryptoServiceProvider();
        private string SecretSalt;

        public HashService(string secretSalt)
        {
            SecretSalt = secretSalt;
        }

        public string Hash(string plaintext)
        {
            byte[] hash = Service.ComputeHash(Encoding.ASCII.GetBytes(SecretSalt + plaintext));
            return Convert.ToBase64String(hash);
        }
    }
}
