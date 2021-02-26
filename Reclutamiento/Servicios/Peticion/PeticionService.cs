using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Utilidades;
using Reclutamiento.Models;
using Reclutamiento.Support;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reclutamiento.Servicios.Peticion
{

    public interface IPeticionService
    {
        Task<HttpResponseMessage> Get(string requestUri);
        Task<HttpResponseMessage> Post(string requestUri, dynamic dyn, string token);
        Task<HttpResponseMessage> PostForm(string requestUri, dynamic dyn);
        Task<HttpResponseMessage> Delete(string requestUri, string token);
        Task<HttpResponseMessage> Put(string requestUri, dynamic dyn, string token);
        Task<HttpResponseMessage> Patch(string requestUri, dynamic dyn, string token);
    }

    public class PeticionService : IPeticionService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UsuarioAplicacion> userManager;
        
        private readonly string host;

        public PeticionService(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<UsuarioAplicacion> userManager, ReclutamientoDbContext contexto)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.host = _configuration.GetValue<string>(Constants.ServiceApi.GHESTIC);
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
        {

            var caracteristicas = _httpContextAccessor.HttpContext.Features.Get<IHttpRequestFeature>();
            if (requestUri == "")
            {
                var uri =  caracteristicas.RawTarget;
                string[] requestUriArray = uri.Split("_");
                var url = requestUriArray[1].Replace("-", "/");
                requestUri = this.host + "/odata/"+ url;
            }
            
            var tokenGhestic = "";
            var tokenPortal = InformacionToken.ObtenerTokenUsuario(_httpContextAccessor);
            if (tokenPortal != "")
            {
                var usuario = InformacionToken.ObtenerInformacionUsuario(tokenPortal,"unique_name", this.userManager);
                
                    tokenGhestic = usuario.TokenGhestic;
               
            }

            var httpContent = new HttpRequestMessage(HttpMethod.Get, $"{requestUri}");
            if (tokenGhestic != "")
            {
                httpContent.Headers.Add("JwtToken", tokenGhestic);
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
            client.MaxResponseContentBufferSize = 1000000;
            return await client.SendAsync(httpContent);
        }

        public async Task<HttpResponseMessage> Post(string requestUri, dynamic dyn, string tokenGhestic)
        {

            string strJSon = JsonConvert.SerializeObject(dyn);

            var httpContent = new HttpRequestMessage(
             HttpMethod.Post, $"{requestUri}");
            if (tokenGhestic != "")
            {
                httpContent.Headers.Add("JwtToken", tokenGhestic);
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

            httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(120);
            client.MaxResponseContentBufferSize = 1000000;
            return await client.SendAsync(httpContent);
        }


        public async Task<HttpResponseMessage> Delete(string requestUri, string tokenGhestic)
        {
            
            var httpContent = new HttpRequestMessage(
            HttpMethod.Delete, $"{requestUri}");
            if (tokenGhestic != "")
            {
                httpContent.Headers.Add("JwtToken", tokenGhestic);
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
            client.MaxResponseContentBufferSize = 1000000;
            return await client.SendAsync(httpContent);
        }

        public async Task<HttpResponseMessage> PostForm(string requestUri, dynamic dyn)
        {
            var client = _httpClient.CreateClient();
            MultipartFormDataContent form = new MultipartFormDataContent();            
            HttpContent  content = new ByteArrayContent(dyn);
            
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = $"{Guid.NewGuid()}"
            };
            form.Add(content);

            var response = await client.PostAsync(requestUri, form);
            return response;
        }


        public async Task<HttpResponseMessage> Put(string requestUri, dynamic dyn, string tokenGhestic)
        {
            string strJSon = JsonConvert.SerializeObject(dyn);
            
            var httpContent = new HttpRequestMessage(
             HttpMethod.Put, $"{requestUri}");
            if (tokenGhestic != "")
            {
                httpContent.Headers.Add("JwtToken", tokenGhestic);
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

            httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(120);
            client.MaxResponseContentBufferSize = 1000000;

            
            return await client.SendAsync(httpContent);
        }


        public async Task<HttpResponseMessage> Patch(string requestUri, dynamic dyn, string tokenGhestic)
        {
            string strJSon = JsonConvert.SerializeObject(dyn);

            var httpContent = new HttpRequestMessage(
             HttpMethod.Patch, $"{requestUri}");
            if (tokenGhestic != "")
            {
                httpContent.Headers.Add("JwtToken", tokenGhestic);
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

            httpContent.Content = new StringContent(strJSon, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(120);
            client.MaxResponseContentBufferSize = 1000000;


            return await client.SendAsync(httpContent);
        }

    }
}
