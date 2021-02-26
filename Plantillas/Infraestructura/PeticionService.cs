using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plantillas.Infraestructura
{

    public interface IPeticionService
    {

        Task<HttpResponseMessage> Get(string requestUri);

        Task<HttpResponseMessage> Post(string requestUri, dynamic dyn, string jwtToken);
    }

    public class PeticionService : IPeticionService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PeticionService(IConfiguration configuration, IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            var httpContent = new HttpRequestMessage(
             HttpMethod.Get, $"{requestUri}");
            foreach (var key in _httpContextAccessor.HttpContext.Request.Headers.Keys)
            {
                try
                {
                    var value = _httpContextAccessor.HttpContext.Request.Headers[key];
                    httpContent.Headers.Add($"{key}", $"{value}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            var client = _httpClient.CreateClient();
            return await client.SendAsync(httpContent);
        }

        public async Task<HttpResponseMessage> Post(string requestUri, dynamic dyn, string jwtToken)
        {
            string strJSon = JsonConvert.SerializeObject(dyn);
            var httpContent = new HttpRequestMessage(
             HttpMethod.Post, $"{requestUri}");
            httpContent.Headers.Add($"JwtToken", $"{jwtToken}");
            foreach (var key in _httpContextAccessor.HttpContext.Request.Headers.Keys)
            {
                try
                {
                    var value = _httpContextAccessor.HttpContext.Request.Headers[key];
                    httpContent.Headers.Add($"{key}", $"{value}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();

            return await client.SendAsync(httpContent);
        }
    }
}
