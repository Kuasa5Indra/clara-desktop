using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Velacro.Basic;

namespace CLARA_Desktop.Model
{
    public class Reservation
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("begin")]
        public string Date_begin { get; set; }
        [JsonProperty("end")]
        public string Date_end { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("asset")]
        public Asset Asset { get; set; }
        [JsonProperty("history")]
        public List<History> Histories { get; set; }
    }
}
