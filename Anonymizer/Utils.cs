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
    }
}
