using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using CLARA_Desktop.Routes;
using System.Net.Http;
using System.IO;
using System.Windows;

namespace CLARA_Desktop.Asset
{
    class AssetController : MyController
    {
        public AssetController(IMyView _myView) : base(_myView)
        {
        }

        public async void CreateAsset(string name, String quantity, MyFile myFile)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(name), "name");
            formContent.Add(new StringContent(quantity), "quantity");
            if (myFile != null)
                formContent.Add(new StreamContent(new MemoryStream(myFile.byteArray)), "image", myFile.fullFileName);
            var request = requestBuilder
                .buildMultipartRequest(new MultiPartContent(formContent))
                .setEndpoint(API.asset)
                .setRequestMethod(HttpMethod.Post);
            var response = await client.sendRequest(request.getApiRequestBundle());
            MessageBox.Show(response.getJObject()["message"].ToString(), "Success");
        }
    }
}
