using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CLARA_Desktop.Model
{
    public class History
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
}
