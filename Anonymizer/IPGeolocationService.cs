using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.IO;

namespace Anonymizer
{
    class IPGeolocationService
    {
        private List<IPGeoData> IPGeoData;

        public IPGeolocationService()
        {
            using(var reader = new StreamReader("ip-geolocation.csv"))
            {
                using(var CSVReader = new CsvReader(reader))
                {
                    IPGeoData = CSVReader.GetRecords<IPGeoCsvData>().Select(data => new IPGeoData(data)).ToList();
                }
            }
        }

        public string GetCountry(string ip)
        {
            int index = IPGeoData.BinarySearch(new IPGeoData(ip));
            return IPGeoData[index].Country;
        }
    }
}
