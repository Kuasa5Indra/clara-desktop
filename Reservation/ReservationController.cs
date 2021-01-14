using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using System.Diagnostics;
using System.IO;
using CLARA_Desktop.Routes;
using Newtonsoft.Json;
using System.Windows;

namespace CLARA_Desktop.Reservation
{
    class ReservationController : MyController
    {
        public List<Model.Reservation> reservationList;
        public ReservationController(IMyView _myView) : base(_myView)
        {

        }

        public async void GetReservationsList()
        {
            var client = new ApiClient(API.URL);
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .setEndpoint(API.reservation)
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));
            client.setOnSuccessRequest(CallbackSuccess);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void SearchReservation(string assetName)
        {
            var client = new ApiClient(API.URL);
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .setEndpoint(API.reservationAsset.Replace("{name}", assetName))
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));
            client.setOnSuccessRequest(CallbackSuccess);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void LoadReservationPage(int currentPage)
        {
            var client = new ApiClient(API.URL);
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .setEndpoint(API.reservationPage.Replace("{number}", currentPage.ToString()))
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));
            client.setOnSuccessRequest(CallbackSuccess);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public void CallbackSuccess(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string json = _response.getJObject()["data"].ToString();
                int currentPage = Int32.Parse(_response.getJObject()["current_page"].ToString());
                int lastPage = Int32.Parse(_response.getJObject()["last_page"].ToString());
                reservationList = JsonConvert.DeserializeObject<List<Model.Reservation>>(json);
                SetContentView(currentPage, lastPage);
            }
        }

        public void SetContentView(int currentPage, int lastPage)
        {
            getView().callMethod("UpdateGrid", reservationList, currentPage, lastPage);
        }

        public async void UpdateStatusReservation(string id, string status, string description)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            var request = requestBuilder
                .buildHttpRequest()
                .addParameters("status", status)
                .addParameters("description", description)
                .setEndpoint(API.reservationId.Replace("{Id}", id))
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));
            Console.WriteLine(request.getApiRequestBundle().getHeaders().convertToJSON());
            var response = await client.sendRequest(request.getApiRequestBundle());
            string json = response.getJObject()["reservation"].ToString();
            Model.Reservation updatedReservation = JsonConvert.DeserializeObject<Model.Reservation>(json);
            MessageBox.Show(response.getJObject()["message"].ToString(), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            getView().callMethod("GetUpdatedReservation", updatedReservation);
        }
    }
}
