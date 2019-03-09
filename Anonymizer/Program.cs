using System;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace Anonymizer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Data> data = DB.GenerateDummyData();

                using(var writer = new StreamWriter("data.csv"))
                {
                    using(var CSVwriter = new CsvWriter(writer))
                    {
                        CSVwriter.WriteRecords(data);
                    }
                }
                Console.WriteLine("Data saved. Output can be found in data.csv in the debug folder.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
