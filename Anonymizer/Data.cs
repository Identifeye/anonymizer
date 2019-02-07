using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymizer
{
    class Data
    {
        public string AccountName { get; set; }
        public string CharacterName { get; set; }
        public string IP { get; set; }
        public string UUID { get; set; }
        public string IPGeolocation { get; set; }
        public bool IsBanned { get; set; }
        public int ActivePlaytime { get; set; }
    }
}
