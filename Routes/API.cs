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
        // public static String URL = "https://api.clara-app.tech/";
        // For Development use localhost
        public static String URL = "http://localhost:8000/";

        // Here's the image path of server
        public static String image_path = String.Concat(URL, "image/");

        /*
         * Here's the list of API routes
         * Register API routes below
         */

        public static String login = "login";
        public static String logout = "logout";
        public static String profile = "profile";
        public static String countStatusReservation = "reservations/count?status={status}";
        public static String reservation = "reservations";
        public static String reservationId = "reservations/{Id}";
        public static String reservationPage = "reservations?page={number}";
        public static String reservationAsset = "reservations/search?asset={name}";
        public static String recentReservation = "reservations?limit=5";
        public static String asset = "assets";
        public static String assetId = "asset/{id}";
        public static String assetName = "asset/search?name={name}";
        public static String assetPage = "asset?page={number}";

    }
}
