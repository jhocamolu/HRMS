using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
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

namespace ApiV3.Dominio.RepresentanteEmpresas.Comandos.Crear
{
    public class CrearRepresentanteEmpresaHandler : IRequestHandler<CrearRepresentanteEmpresaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;

        public CrearRepresentanteEmpresaHandler(NominaDbContext contexto, IConfiguration configuration, IPeticionService peticion)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.PLANTILLAS);
        }

        public async Task<CommandResult> Handle(CrearRepresentanteEmpresaRequest request, CancellationToken cancellationToken)
        {
            try
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
                var cargo = contexto.FuncionarioDatoActuales.FirstOrDefault(x => x.Id == request.FuncionarioId);
                RepresentanteEmpresa representante = new RepresentanteEmpresa
                {
                    FuncionarioId = (int)request.FuncionarioId,
                    CargoId = (int)cargo.CargoId,
                    GrupoDocumentoSlug = request.GrupoDocumentoSlug,
                    FechaInicio = (DateTime)request.FechaInicio,
                    FechaFin = (DateTime)request.FechaFin
                };

                this.contexto.RepresentanteEmpresas.Add(representante);
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
