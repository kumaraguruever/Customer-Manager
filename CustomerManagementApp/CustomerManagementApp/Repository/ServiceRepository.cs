using System;
using System.Configuration;
using System.Net.Http;

namespace CustomerManagementApp.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public HttpClient Client { get;}
        public ServiceRepository(HttpClient httpClient)
        {
            Client = httpClient ?? new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
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