using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymizer
{
    /// <summary>
    /// IPGeoCsvData but with different values so that it's easier to work with.
    /// It makes use of preprocessing to avoid redundant ip string parsing.
    /// </summary>
    class IPGeoData : IComparable
    {
        public uint From { get; private set; }
        public uint To { get; private set; }
        public uint ExactIP { get; private set; }
        public string Country { get; private set; }

        private bool isRange;
        
        public IPGeoData(IPGeoCsvData data)
        {
            From = Utils.IPStringToInteger(data.From);
            To = Utils.IPStringToInteger(data.To);
            Country = data.Country;
            isRange = true;
        }

        public IPGeoData(string ip)
        {
            ExactIP = Utils.IPStringToInteger(ip);
            isRange = false;
        }

        /// <summary>
        /// Checks whether an IP is in a range, before it, or after it.
        /// </summary>
        public int CompareTo(object obj)
        {
            IPGeoData other = (IPGeoData)obj;

            if(!isRange && !other.isRange) return ExactIP.CompareTo(other.ExactIP); // Compare IP to IP
            if(isRange && other.isRange) return 0; // Unexpected behavior
            if(isRange) return -other.CompareTo(this); // Make sure this object is the player's ip

            if(ExactIP < other.From) return -1;
            if(ExactIP > other.To) return 1;
            return 0;
        }
    }
}
