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

namespace CLARA_Desktop.Reservation
{
    class ReservationController : MyController
    {
        public MyList<Model.Reservation> reservationList;
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
                reservationList = JsonConvert.DeserializeObject<MyList<Model.Reservation>>(json);
                SetContentView(currentPage, lastPage);
            }
        }

        public void SetContentView(int currentPage, int lastPage)
        {
            getView().callMethod("UpdateGrid", reservationList, currentPage, lastPage);
        }
    }
}
