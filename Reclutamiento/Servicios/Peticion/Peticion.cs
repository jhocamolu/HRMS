using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reclutamiento.Servicios.Peticion
{
    public abstract class Peticion
    {

        protected IConfiguration configuration;
        protected IPeticionService peticion;

        protected async Task<dynamic> ObtenerServicios(string url = "")
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Get(url);
                return httpResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected async Task<dynamic> EnviarServicios(string url, object param, string token)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Post(url, param, token);
                return httpResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected async Task<dynamic> ActualizarServicios(string url, object param, string token)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Put(url, param, token);
                return httpResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected async Task<dynamic> ParcialServicios(string url, object param, string token)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Patch(url, param, token);
                return httpResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected async Task<dynamic> EliminarServicios(string url, string token)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Delete(url, token);
                return httpResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
