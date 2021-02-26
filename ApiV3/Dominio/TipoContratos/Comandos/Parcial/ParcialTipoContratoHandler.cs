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

namespace ApiV3.Dominio.TipoContratos.Comandos.Parcial
{
    public class ParcialTipoContratoHandler : IRequestHandler<ParcialTipoContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;

        public ParcialTipoContratoHandler(NominaDbContext contexto, IConfiguration configuration, IPeticionService peticion)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.PLANTILLAS);
        }

        public async Task<CommandResult> Handle(ParcialTipoContratoRequest request, CancellationToken cancellationToken)
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

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoContrato.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoContrato.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (!String.IsNullOrEmpty(request.Nombre))
                {
                    tipoContrato.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                }
                if (request.Clase != null)
                {
                    tipoContrato.Clase = (ClaseTipoContrato)request.Clase;
                }
                if (request.CantidadProrrogas != null)
                {
                    tipoContrato.CantidadProrrogas = Convert.ToInt32(request.CantidadProrrogas.ToString());
                }
                if (request.DuracionMaxima != null)
                {
                    tipoContrato.DuracionMaxima = Convert.ToInt32(request.DuracionMaxima.ToString());
                }
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
                if (!String.IsNullOrEmpty(request.DocumentoSlug))
                {
                    tipoContrato.DocumentoSlug = request.DocumentoSlug;
                }
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
