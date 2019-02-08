using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymizer
{
    static class Utils
    {
        public static string ReadLine(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
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
