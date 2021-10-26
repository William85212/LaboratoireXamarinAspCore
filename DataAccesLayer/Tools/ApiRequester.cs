using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DataAccesLayer.Tools
{
    public class ApiRequester
    {
        private string _baseAddress;

        private HttpClient _client;

        public HttpClient Client
        {
            get { return _client; }
        }

        public ApiRequester(string baseAdress)
        {
            _client = new HttpClient();
            _baseAddress = baseAdress;
            _client.BaseAddress = new Uri(_baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public TResult Get<TResult>(string url, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = _client.GetAsync(url).Result)
            {
                if (!message.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }

                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResult>(json);
            }
        }

        public void Post<TEntity>(string url, TEntity entity, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = _client.PostAsync(url, content).Result)
            {
                if (!message.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }

        public void Delete(string url, int id, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = _client.DeleteAsync(url + id).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }

        public void Update<TEntity>(string url, int id, TEntity entity, string token = null)
        {

            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = _client.PutAsync(url + id, content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }

        public void Update<TEntity>(string url, TEntity entity, string token = null)
        {

            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = _client.PutAsync(url , content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }
    }
}
