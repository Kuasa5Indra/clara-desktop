using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using CLARA_Desktop.Dashboard;

namespace CLARA_Desktop.Login
{
    class LoginController : MyController
    {
        public LoginController(IMyView _myView) : base(_myView)
        {

        }

        public async void Login(String email, String password)
        {
            var client = new ApiClient("http://localhost:8000/");
            var requestBuilder = new ApiRequestBuilder();

            var request = requestBuilder
                .buildHttpRequest()
                .addParameters("email", email)
                .addParameters("password", password)
                .setEndpoint("login")
                .setRequestMethod(HttpMethod.Post);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getJObject()["token"]);
            File.WriteAllText("jwt.txt", response.getJObject()["token"].ToString());
            getView().callMethod("RouteToDashboard");
        }
    }
}
