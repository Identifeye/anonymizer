using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymizer
{
    class HashService
    {
        private string SecretSalt;

        public HashService(string secretSalt)
        {
            SecretSalt = secretSalt;
        }

        public string Hash(string plaintext)
        {
            // We are cutting off the hash from the result because we do not want nor need it. You can now only crack any data if you have the hash yourself.
            // Prevents non-server people from looking up hashes for known usernames and checking for a relation.
            return BCrypt.Net.BCrypt.HashPassword(plaintext, SecretSalt).Substring(SecretSalt.Length);
        }
    }
}
