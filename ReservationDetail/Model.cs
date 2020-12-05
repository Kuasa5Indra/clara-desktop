using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Basic;

namespace CLARA_Desktop.ReservationDetail
{
    public class Model
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class User
        {
            public string Id { get; set; }
            public string Full_Name { get; set; }
            public string Nrp { get; set; }
            public string Class { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public class Asset
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public int Quantity { get; set; }
        }

        
        public class History
        {
            public string Status { get; set; }
            public string Datetime { get; set; }
        }

        public class Reservation
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public string Begin { get; set; }
            public string End { get; set; }
            public User User { get; set; }
            public Asset Asset { get; set; }
            public MyList<History> History { get; set; }
        }


    }
}
