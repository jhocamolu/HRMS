using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiV3.Servicios.Peticion
{

    public interface IPeticionService
    {
        Task<HttpResponseMessage> Get(string requestUri);
        Task<HttpResponseMessage> Post(string requestUri, dynamic dyn);
        Task<HttpResponseMessage> PostForm(string requestUri, dynamic dyn);
        Task<HttpResponseMessage> Delete(string requestUri);
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
            var hasToken = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("jwttoken", out var token);
            var httpContent = new HttpRequestMessage(
            HttpMethod.Get, $"{requestUri}");
            if (token != "" && String.IsNullOrEmpty(token))
            {
                httpContent.Headers.Add($"JwtToken", $"{token}");
            }
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
            client.Timeout = TimeSpan.FromSeconds(120);
            return await client.SendAsync(httpContent);
        }

        public async Task<HttpResponseMessage> Post(string requestUri, dynamic dyn)
        {
            string strJSon = JsonConvert.SerializeObject(dyn);
            var httpContent = new HttpRequestMessage(
             HttpMethod.Post, $"{requestUri}");
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
            client.Timeout = new TimeSpan(0, 15, 0);
            return await client.SendAsync(httpContent);
        }


        public async Task<HttpResponseMessage> Delete(string requestUri)
        {
            var httpContent = new HttpRequestMessage(
            HttpMethod.Delete, $"{requestUri}");
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

        public async Task<HttpResponseMessage> PostForm(string requestUri, dynamic dyn)
        {
            var client = _httpClient.CreateClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent content = new ByteArrayContent(dyn);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = $"{Guid.NewGuid()}"
            };
            form.Add(content);

            var response = await client.PostAsync(requestUri, form);
            return response;
        }
    }
}
