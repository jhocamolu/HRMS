using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RepresentanteEmpresas.Comandos.Parcial
{
    public class ParcialRepresentanteEmpresaHandler : IRequestHandler<ParcialRepresentanteEmpresaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;

        public ParcialRepresentanteEmpresaHandler(NominaDbContext contexto, IConfiguration configuration, IPeticionService peticion)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.PLANTILLAS);
        }

        public async Task<CommandResult> Handle(ParcialRepresentanteEmpresaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var representante = contexto.RepresentanteEmpresas.FirstOrDefault(x => x.Id == request.Id);

                if (request.GrupoDocumentoSlug != null)
                {
                    JObject data = await this.ObtenerServicios($"{host}/odata/grupodocumentos?$filter=slug eq '{request.GrupoDocumentoSlug}'");
                    if (data == null) return CommandResult.Fail("Error al conectar con el servicio.", 500);

                    if (!data.ContainsKey("value"))
                    {
                        if (data["value"]["slug"] == null)
                        {
                            return CommandResult.Fail("El grupo de documentos que intentas guardar no existe.", 400);
                        }
                    }
                    representante.GrupoDocumentoSlug = request.GrupoDocumentoSlug;
                }

                //if (request.FuncionarioId != null)
                //{
                //    representante.FuncionarioId = (int)request.FuncionarioId;
                //}

                if (request.FechaInicio != null && request.FechaFin != null)
                {
                    representante.FechaInicio = (DateTime)request.FechaInicio;
                    representante.FechaFin = (DateTime)request.FechaFin;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        representante.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        representante.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }


                this.contexto.RepresentanteEmpresas.Update(representante);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(representante);
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