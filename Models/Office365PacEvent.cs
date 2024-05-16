using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Office365.PacUtil.Models
{
    public class Office365PacEvent
    {
        public string Id { get; set; }
        public string ServiceArea { get; set; }
        public List<string> Urls { get; set; }
        public List<Tuple<string, string>> IpRanges { get; set; }
    }
}
