using System;
using System.Net.Http;

namespace CustomerManagementApp.Repository
{
    public interface IServiceRepository
    {
        Uri BaseAddress { get; set; }
        HttpClient Client { get;}

        HttpResponseMessage DeleteResponse(string url);
        HttpResponseMessage GetResponse(string url);
        HttpResponseMessage PostResponse(string url, object model);
        HttpResponseMessage PutResponse(string url, object model);
    }
}