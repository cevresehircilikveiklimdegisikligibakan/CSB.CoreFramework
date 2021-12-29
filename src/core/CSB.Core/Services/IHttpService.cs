using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSB.Core.Services
{
    public interface IHttpService
    {
        bool IsHttps(Uri uri, bool? forceIsHttps = null);
        string GetIPAddress(IHttpContextAccessor httpContextAccessor);

        Task<TEntity> GetAsync<TEntity>(string request) where TEntity : class, new();
        Task<HttpResponseMessage> PostAsync<TEntity>(string requestUri, TEntity data) where TEntity : class, new();
        Task<HttpResponseMessage> PutAsync<TEntity>(string requestUri, TEntity data) where TEntity : class, new();
        Task<TEntity> DeleteAsync<TEntity>(string requestUri) where TEntity : class, new();
    }
}