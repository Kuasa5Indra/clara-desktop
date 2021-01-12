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
using Newtonsoft.Json;

namespace CLARA_Desktop.Asset
{
    class AssetController : MyController
    {
        public AssetController(IMyView _myView) : base(_myView)
        {
        }

        public async void LoadAsset()
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.assets)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(CallbackSuccess);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void LoadAssetPage(int currentPage)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.assetPage.Replace("{number}", currentPage.ToString()))
                .setRequestMethod(HttpMethod.Get);
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
                List<Model.Asset> assets = JsonConvert.DeserializeObject<List<Model.Asset>>(json);
                getView().callMethod("SetAssetListView", SetImagePath(assets), currentPage, lastPage);
            }
        }

        private List<Model.Asset> SetImagePath(List<Model.Asset> assets)
        {
            for(int i = 0; i < assets.Count; i++)
            {
                assets[i].Image = String.Concat(API.image_path, assets[i].Image);
            }
            return assets;
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

        public async void UpdateAsset(Model.Asset newAsset, MyFile myFile)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(newAsset.Name), "name");
            formContent.Add(new StringContent(newAsset.Quantity.ToString()), "quantity");
            if (myFile != null)
                formContent.Add(new StreamContent(new MemoryStream(myFile.byteArray)), "image", myFile.fullFileName);
            var request = requestBuilder
                .buildMultipartRequest(new MultiPartContent(formContent))
                .setEndpoint(API.assetId.Replace("{id}", newAsset.Id))
                .setRequestMethod(HttpMethod.Post);
            var response = await client.sendRequest(request.getApiRequestBundle());
            MessageBox.Show(response.getJObject()["message"].ToString(), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async void DeleteAsset(string id)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.assetId.Replace("{id}", id))
                .setRequestMethod(HttpMethod.Delete);
            var response = await client.sendRequest(request.getApiRequestBundle());
            MessageBox.Show(response.getJObject()["message"].ToString(), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            getView().callMethod("RouteToAssetPage");
        }

        public async void SearchAsset(string name)
        {
            var client = new ApiClient(API.URL);
            var requestBuilder = new ApiRequestBuilder();
            client.setAuthorizationToken(File.ReadAllText("jwt.txt"));

            var request = requestBuilder.buildHttpRequest()
                .setEndpoint(API.assetName.Replace("{name}", name))
                .setRequestMethod(HttpMethod.Get);
            var response = await client.sendRequest(request.getApiRequestBundle());
            List<Model.Asset> assets = response.getParsedObject<List<Model.Asset>>();
            getView().callMethod("SetSearchAsset", SetImagePath(assets));
        }
    }
}
