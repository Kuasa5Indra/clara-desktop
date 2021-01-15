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
using CLARA_Desktop.Routes;
using System.Windows;

namespace CLARA_Desktop.Login
{
    class LoginController : MyController
    {
        public LoginController(IMyView _myView) : base(_myView)
        {

        }

        public async void Login(String email, String password)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();

            var request = requestBuilder
                .buildHttpRequest()
                .addParameters("email", email)
                .addParameters("password", password)
                .setEndpoint(API.login)
                .setRequestMethod(HttpMethod.Post);
            client.setOnFailedRequest(FailedLogin);
            client.setOnSuccessRequest(SuccessLogin);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public void FailedLogin(HttpResponseBundle _response)
        {
            MessageBox.Show("Invalid email or password", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        public void SuccessLogin(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                if (_response.getJObject()["role"].ToString() != "Lecturer")
                {
                    MessageBox.Show("Sorry, This application is only for Lecturer Access", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    File.WriteAllText("jwt.txt", _response.getJObject()["token"].ToString());
                    getView().callMethod("RouteToDashboard");
                }
            }
        }
    }
}
