using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContratos.Comandos.Actualizar
{
    public class ActualizarTipoContratoHandler : IRequestHandler<ActualizarTipoContratoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;

        public ActualizarTipoContratoHandler(NominaDbContext contexto, IConfiguration configuration, IPeticionService peticion)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.PLANTILLAS);
        }

        public async Task<CommandResult> Handle(ActualizarTipoContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                JObject data = await this.ObtenerServicios($"{host}/odata/documentos?$filter=slug eq '{request.DocumentoSlug}'");
                if (data == null) return CommandResult.Fail("Error al conectar con el servicio.", 500);

                if (!data.ContainsKey("value"))
                {
                    if (data["value"]["slug"] == null)
                    {
                        return CommandResult.Fail("El documento que intentas guardar no existe.", 400);
                    }
                }

                TipoContrato tipoContrato = this.contexto.TipoContratos.Find(request.Id);

                tipoContrato.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                tipoContrato.Clase = (ClaseTipoContrato)request.Clase;
                tipoContrato.CantidadProrrogas = Convert.ToInt32(request.CantidadProrrogas);
                tipoContrato.DuracionMaxima = Convert.ToInt32(request.DuracionMaxima);
                if (request.TerminoIndefinido != null)
                {
                    if (request.TerminoIndefinido == true)
                    {
                        tipoContrato.TerminoIndefinido = true;
                    }
                    else
                    {
                        tipoContrato.TerminoIndefinido = false;
                    }

                }

                tipoContrato.DocumentoSlug = request.DocumentoSlug;

                this.contexto.TipoContratos.Update(tipoContrato);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoContrato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }


        private async Task<JObject> ObtenerServicios(string url)
        {
            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Get(url);
                if (!httpResponse.IsSuccessStatusCode) return null;
                return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
