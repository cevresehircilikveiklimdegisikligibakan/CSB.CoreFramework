using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSB.Core.Services
{
    internal class HttpService : IHttpService
    {
        private HttpClient HttpClient { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }
        private ISerializer Serializer { get; }

        public HttpService(IHttpContextAccessor httpContextAccessor,
                            ISerializer serializer)
        {
            HttpClient = new HttpClient();
            HttpContextAccessor = httpContextAccessor;
            Serializer = serializer;
        }

        public string GetIPAddress(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null || httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.Request == null)
            {
                return GetLocalIpAddress();
            }

            string address = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (address == "::1" || address == "0.0.0.0" || address == "127.0.0.1")
            {
                return GetLocalIpAddress();
            }

            return address;
        }

        public bool IsHttps(Uri uri, bool? forceIsHttps = null)
        {
            return forceIsHttps.HasValue ? forceIsHttps.Value : uri.ToString().StartsWith("https://");
        }

        public async Task<TEntity> GetAsync<TEntity>(string request) where TEntity : class, new()
        {
            var result = await HttpClient.GetAsync(request);
            if (!result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result;

                throw new Exception(data);
            }
            var resultContent = await result.Content.ReadAsStringAsync(); //TODO: status code vs eklenecek.
            return Serializer.Deserialize<TEntity>(resultContent);
        }
        public async Task<HttpResponseMessage> PostAsync<TEntity>(string requestUri, TEntity data) where TEntity : class, new()
        {
            var result = await HttpClient.PostAsync(requestUri, GetHttpContent(data));
            if (!result.IsSuccessStatusCode)
            {
                var serviceResult = result.Content.ReadAsStringAsync().Result;

                throw new Exception(serviceResult);
            }
            return result;
        }
        public async Task<HttpResponseMessage> PutAsync<TEntity>(string requestUri, TEntity data) where TEntity : class, new()
        {
            var result = await HttpClient.PutAsync(requestUri, GetHttpContent(data));
            if (!result.IsSuccessStatusCode)
            {
                var serviceResult = result.Content.ReadAsStringAsync().Result;

                throw new Exception(serviceResult);
            }
            return result;
        }
        public async Task<TEntity> DeleteAsync<TEntity>(string requestUri) where TEntity : class, new()
        {
            var result = await HttpClient.DeleteAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result;

                throw new Exception(data);
            }
            var resultContent = await result.Content.ReadAsStringAsync(); //TODO: status code vs eklenecek.
            return Serializer.Deserialize<TEntity>(resultContent);
        }

        public HttpContent GetHttpContentFromJson(string contentJson)
        {
            return new StringContent(contentJson, Encoding.UTF8, "application/json");
        }

        #region #Private

        private string GetLocalIpAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }
        }
        private HttpContent GetHttpContent<TEntity>(TEntity data)
        {
            string contentJson = Serializer.Serialize<TEntity>(data);
            return GetHttpContentFromJson(contentJson);
        }


        #endregion
    }
}