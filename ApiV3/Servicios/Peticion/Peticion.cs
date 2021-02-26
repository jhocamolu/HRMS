using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiV3.Servicios.Peticion
{
    public abstract class Peticion
    {

        protected IConfiguration configuration;
        protected IPeticionService peticion;

        protected async Task<JObject> ObtenerServicios(string url)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Get(url);
                if (!httpResponse.IsSuccessStatusCode) return null;
                return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected async Task<JObject> EnviarServicios(string url, object param)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Post(url, param);
                if (!httpResponse.IsSuccessStatusCode) return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
