using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLARA_Desktop.Routes
{
    class API
    {
        // For Production use CLARA Rest API
        // public static String URL = "https://api.clara-app.tech";
        // For Development use localhost
        public static String URL = "http://localhost:8000/";

        /*
         * Here's the list of API routes
         * Register API routes below
         */

        public static String login = "login";
        public static String logout = "logout";
        public static String countStatusReservation = "reservations/count?status={status}";
        public static String recentReservation = "reservations?limit=5";
    }
}
