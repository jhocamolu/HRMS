using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Actualizar
{
    public class ActualizarInformacionFamiliarHandler : IRequestHandler<ActualizarInformacionFamiliarRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarInformacionFamiliarHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarInformacionFamiliarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                InformacionFamiliar informacionFamiliar = contexto.InformacionFamiliares.Find(request.Id);

                informacionFamiliar.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                informacionFamiliar.SegundoNombre = Texto.LetraCapital(request.SegundoNombre);
                informacionFamiliar.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                informacionFamiliar.SegundoApellido = Texto.LetraCapital(request.SegundoApellido);
                informacionFamiliar.SexoId = request.SexoId;
                informacionFamiliar.ParentescoId = request.ParentescoId;
                informacionFamiliar.Dependiente = (bool)request.Dependiente;
                informacionFamiliar.FechaNacimiento = request.FechaNacimiento;
                informacionFamiliar.TipoDocumentoId = request.TipoDocumentoId;
                informacionFamiliar.NumeroDocumento = request.NumeroDocumento;
                informacionFamiliar.NivelEducativoId = request.NivelEducativoId;
                informacionFamiliar.TelefonoFijo = request.TelefonoFijo;
                informacionFamiliar.Celular = request.Celular;
                informacionFamiliar.DivisionPoliticaNivel2Id = request.DivisionPoliticaNivel2Id;
                informacionFamiliar.Direccion = request.Direccion;

                informacionFamiliar.Estado = EstadoInformacionFuncionario.Validado;
                if (headers[headKeyEsMovil].ToString() != null && headers[headKeyEsMovil].ToString() == secretMovil)
                {
                    informacionFamiliar.Estado = EstadoInformacionFuncionario.Pendiente;
                }
                contexto.InformacionFamiliares.Update(informacionFamiliar);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(informacionFamiliar);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
