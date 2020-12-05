using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using System.Diagnostics;

namespace CLARA_Desktop.Reservation
{
    class ReservationController : MyController
    {
        public ReservationController(IMyView _myView) : base(_myView)
        {
            getReservationsList();
        }

        public async void getReservationsList()
        {
            var client = new ApiClient("https://clara-app.tech/api/reservations?limit=5");
            var request = new ApiRequestBuilder();
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvY2xhcmEtYXBwLnRlY2hcL2FwaVwvbG9naW4iLCJpYXQiOjE2MDY5MTUyNTMsIm5iZiI6MTYwNjkxNTI1MywianRpIjoiS25FbUxTWUZ1ODlyWWFDUCIsInN1YiI6IjVmYzVmMGRiMmI3MjAwMDBkYzAwMGExOCIsInBydiI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.oTzriFjIVB1nzSmznWY3mah5K32od1hOBHYNH72VwXw";
            var req = request
                .buildHttpRequest()
                //.addHeaders("token", token)
                //.setEndpoint("api/reservations?limit=5")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(callbackSuccess);
            //client.setOnFailedRequest(setReservationListContent);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Debug.WriteLine("asdf");


        }
        
        public MyList<Model.Reservation> reservationList;

        public void callbackSuccess(HttpResponseBundle _response)
        {
            Debug.WriteLine("masuk callback api");
            if (_response.getHttpResponseMessage().Content != null)
            {
                //Console.WriteLine(_response.getHttpResponseMessage());
                //Debug.WriteLine(_response.getHttpResponseMessage());
                Debug.WriteLine(_response.getParsedObject<MyList<Model.Reservation>>());
                reservationList = _response.getParsedObject<MyList<Model.Reservation>>();
                setContentView();
            }
        }

        public void setContentView()
        {
            getView().callMethod("updateGrid", reservationList);
        }
    }
}
