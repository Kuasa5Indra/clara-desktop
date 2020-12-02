using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CLARA_Desktop.Model
{
    public class User
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
        [JsonProperty("nrp")]
        public int Nrp { get; set; }
        [JsonProperty("grade")]
        public string Grade { get; set; }
        [JsonProperty("nip")]
        public string Nip { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
