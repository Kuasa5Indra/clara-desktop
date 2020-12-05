using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLARA_Desktop.Reservation
{
    public class Model
    {
        //model
        public class User
        {
            public string _id { get; set; }
            public string full_name { get; set; }
            public string image { get; set; }
            public int nrp { get; set; }
            public string @class { get; set; }
            public string email { get; set; }
            public string role { get; set; }
        }

        public class Asset
        {
            public string _id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string image { get; set; }
        }

        public class Reservation
        {
            public string _id { get; set; }
            public string description { get; set; }
            public string begin { get; set; }
            public string end { get; set; }
            public User user { get; set; }
            public Asset asset { get; set; }
            public string status { get; set; }
        }
    }
}
