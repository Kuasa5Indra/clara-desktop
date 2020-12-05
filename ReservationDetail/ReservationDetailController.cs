using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace CLARA_Desktop.ReservationDetail
{
    class ReservationDetailController : MyController
    {
        public ReservationDetailController(IMyView _myView) : base(_myView)
        {
            getReservationDetail();
        }

        public async void getReservationDetail()
        {
            var reservation_id = "5fc79155139559663551cdc2";
            var client = new ApiClient("https://api.clara-app.tech/reservations/" + reservation_id);
            var request = new ApiRequestBuilder();
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvYXBpLmNsYXJhLWFwcC50ZWNoXC9sb2dpbiIsImlhdCI6MTYwNzA4NjQzNSwibmJmIjoxNjA3MDg2NDM1LCJqdGkiOiJPZk4zbTBSMXNvTWs0RzNoIiwic3ViIjoiNWZjNWYwZGIyYjcyMDAwMGRjMDAwYTE4IiwicHJ2IjoiODdlMGFmMWVmOWZkMTU4MTJmZGVjOTcxNTNhMTRlMGIwNDc1NDZhYSJ9.gfBQz9rz7BjBs2_DEWA0V6POWU6Ba_jsbel7r_wBeLA";
            var req = request
                .buildHttpRequest()
                //.addHeaders("token", token)
                //.setEndpoint("api/reservations?limit=5")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(callbackSuccess);
            //client.setOnFailedRequest(setReservationListContent);
            var response = await client.sendRequest(request.getApiRequestBundle());

        }

        public Model.Reservation reservationDetail;

        public void callbackSuccess(HttpResponseBundle _response)
        {
            Debug.WriteLine("masuk callback api");
            if (_response.getHttpResponseMessage().Content != null)
            {
                //Console.WriteLine(_response.getHttpResponseMessage());
                //Debug.WriteLine(_response.getHttpResponseMessage());
                //Debug.WriteLine(_response.getJObject());
                //Debug.WriteLine(_response.getParsedObject<Model.Reservation>());
                //reservationDetail = _response.getParsedObject<MyList<Model.Reservation>>();
                reservationDetail = _response.getParsedObject<Model.Reservation>();
                setContentView();
            }
        }

        public void setContentView()
        {
            getView().callMethod("updateStatusGrid", reservationDetail);
        }
    }
}
