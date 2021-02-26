using ApiV3.Infraestructura.DbContexto;
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

namespace ApiV3.Dominio.RepresentanteEmpresas.Comandos.Actualizar
{
    public class ActuailzarRepresentanteEmpresaHandler : IRequestHandler<ActuailzarRepresentanteEmpresaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;

        public ActuailzarRepresentanteEmpresaHandler(NominaDbContext contexto, IConfiguration configuration, IPeticionService peticion)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.PLANTILLAS);
        }

        public async Task<CommandResult> Handle(ActuailzarRepresentanteEmpresaRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var representante = contexto.RepresentanteEmpresas.FirstOrDefault(x => x.Id == request.Id);

                JObject data = await this.ObtenerServicios($"{host}/odata/grupodocumentos?$filter=slug eq '{request.GrupoDocumentoSlug}'");
                if (data == null) return CommandResult.Fail("Error al conectar con el servicio.", 500);

                if (!data.ContainsKey("value"))
                {
                    if (data["value"]["slug"] == null)
                    {
                        return CommandResult.Fail("El grupo de documentos que intentas guardar no existe.", 400);
                    }
                }

                //representante.FuncionarioId = (int)request.FuncionarioId;
                representante.GrupoDocumentoSlug = request.GrupoDocumentoSlug;
                representante.FechaInicio = (DateTime)request.FechaInicio;
                representante.FechaFin = (DateTime)request.FechaFin;

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
