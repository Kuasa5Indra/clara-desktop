using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace CLARA_Desktop.Dashboard
{
    class DashboardController : MyController
    {
        public DashboardController(IMyView _myView) : base(_myView)
        {

        }

        public async void Logout()
        {
            var client = new ApiClient("http://localhost:8000/");
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint("logout")
                .setRequestMethod(HttpMethod.Get);

            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getJObject()["message"]);
            client.clearAuthorizationToken();
            getView().callMethod("OnClickLogout");
        }
    }
}
