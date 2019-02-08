using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Anonymizer
{
    static class Utils
    {
        private static SHA512CryptoServiceProvider HashService = new SHA512CryptoServiceProvider();

        public static string ReadLine(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        public static string Hash(string plaintext)
        {
            byte[] hash = HashService.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Converts an IP to an integer so that it is easier to compare
        /// </summary>
        public static uint IPStringToInteger(string ip)
        {
            uint ret = 0;
            string[] numbers = ip.Split('.');
            foreach(string n in numbers)
            {
                ret *= 256;
                ret += uint.Parse(n);
            }
            return ret;
        }
    }
}
