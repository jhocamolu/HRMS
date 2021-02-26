using ApiV3.Servicios.RequestData;
using ApiV3.Support;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace ApiV3.Servicios.Autenticacion
{
    public class AutenticacionService : IAutenticacionService
    {

        private readonly IHttpClientFactory httpClient;
        private readonly IConfiguration configuration;
        private readonly IRequestData requestData;

        public AutenticacionService(IConfiguration configuration, IHttpClientFactory httpClient, IRequestData requestData)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.requestData = requestData ?? throw new ArgumentNullException(nameof(requestData));
        }


        /*Método valida si el token es un token valido*/
        public bool TokenValido(string token, string permiso = null)
        {
            //EndPoint de la petición ValidarToken
            var url = configuration.GetValue<string>(Constants.Peticion.VALIDATIONENDPOINT);
            var httpContent = new HttpRequestMessage(
                    HttpMethod.Post, url);
            httpContent.Headers.Add("Accept", "application/json");
            //Adiciona Token a la petición
            httpContent.Headers.Add("JwtToken", token);
            httpContent.Headers.Add("Aplicacion", configuration.GetValue<string>(Constants.Peticion.NOMBREAPLICACIONENAUTENTICACION));
            if (permiso != null)
            {
                httpContent.Headers.Add("Permiso", permiso);
            }
            var client = httpClient.CreateClient();
            var response = client.SendAsync(httpContent).Result;
            //response.EnsureSuccessStatusCode();
            //Obtiene respuesta de la petición
            var content = response.Content.ReadAsStringAsync();
            //Parcea peticion tipo bool
            var usuApp = Convert.ToBoolean(content.Result);

            client.Dispose();
            return usuApp;
        }
    }
}
