using System;
using System.Configuration;
using System.Net.Http;

namespace CustomerManagementApp.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private static readonly HttpClient __HttpClient = new HttpClient();

        public HttpClient Client 
        {
            get
            {
                if (__HttpClient.BaseAddress == null)
                {
                    __HttpClient.BaseAddress = BaseAddress; 
                }
                return __HttpClient;
            }
        }
        public Uri BaseAddress { get; set; }
        public ServiceRepository()
        {
        }
        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}