using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymizer
{
    /// <summary>
    /// Data read directly from ip-geolocation.csv
    /// </summary>
    class IPGeoCsvData
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Country { get; set; }
    }
}
