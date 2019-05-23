using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Puppy.Model.Output;
using Puppy.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Puppy.Utils
{
    public class RestApi
    {
        HttpClient client = new HttpClient();
        private string _hostUrl ="";
        private const string JSON_CONTENT = "application/json";


        public RestApi(string hostUrl)
        {
            this._hostUrl = hostUrl;
            client.BaseAddress = new Uri(hostUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(JSON_CONTENT));
        }

        public void SetHeader(string key, string value)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<HttpResponseMessage> AddAsync(string data)
        {
            HttpResponseMessage response = await client.PostAsync(_hostUrl, new StringContent(data, Encoding.UTF8, JSON_CONTENT));
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string queryString)
        {
            HttpResponseMessage response = await client.GetAsync(_hostUrl + (String.IsNullOrEmpty(queryString)?"":queryString)) ;
            return response;
        }


        public async Task<HttpResponseMessage> EditAsync(string data)
        {
            HttpResponseMessage response = await client.PutAsync(_hostUrl, new StringContent(data, Encoding.UTF8, JSON_CONTENT));
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string data)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, this._hostUrl);
            request.Content = new StringContent(data, Encoding.UTF8, JSON_CONTENT);
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string data)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, this._hostUrl);
            request.Content = new StringContent(data, Encoding.UTF8, JSON_CONTENT);
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }

        public async Task<string> GetOneAsync(HttpMethod method, string data)
        {
            HttpResponseMessage response = await this.SendAsync(method, data);
        
            var responseBody = await response.Content.ReadAsStringAsync();
            //Result result = JsonConvert.DeserializeObject<Result>(responseBody);
            return responseBody;
        }

        public async Task<List<IModel>> GetManyAsync(HttpMethod method, string data)
        {
            HttpResponseMessage response = await this.SendAsync(method, data);
        
            var responseBody = await response.Content.ReadAsStringAsync();
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);
            return JsonConvert.DeserializeObject<List<IModel>>(result.Data);
        }
    }
}
