using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using CLARA_Desktop.Routes;
using System.IO;
using System.Net.Http;

namespace CLARA_Desktop.Home
{
    class HomePageController : MyController
    {
        public HomePageController(IMyView _myView) : base(_myView)
        {

        }

        public async void CountWaitingReservation()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.countStatusReservation.Replace("{status}", "waiting"))
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            getView().callMethod("SetWaitingReservationLabel", (int)response.getJObject()["count"]);
        }

        public async void CountOnReserveReservation()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.countStatusReservation.Replace("{status}", "reserve"))
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            getView().callMethod("SetOnReserveReservationLabel", (int)response.getJObject()["count"]);
        }

        public async void CountReturnedReservation()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.countStatusReservation.Replace("{status}", "return"))
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            getView().callMethod("SetReturnedReservationLabel", (int)response.getJObject()["count"]);
        }

        public async void CountDeniedReservation()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.countStatusReservation.Replace("{status}", "denied"))
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            /*getView().callMethod("SetReturnedReservationLabel", (int)response.getJObject()["count"]);*/
        }

        public async void LoadRecentReservation()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.recentReservation)
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            List<Model.Reservation> reservations = response.getParsedObject<List<Model.Reservation>>();
            getView().callMethod("SetRecentReservationListView", reservations);
        }
    }
}
