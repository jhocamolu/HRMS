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

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Crear
{
    public class CrearInformacionFamiliarHandler : IRequestHandler<CrearInformacionFamiliarRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public CrearInformacionFamiliarHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearInformacionFamiliarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                InformacionFamiliar informacionFamiliar = new InformacionFamiliar
                {
                    FuncionarioId = request.FuncionarioId,
                    PrimerNombre = Texto.LetraCapital(request.PrimerNombre),
                    SegundoNombre = Texto.LetraCapital(request.SegundoNombre),
                    PrimerApellido = Texto.LetraCapital(request.PrimerApellido),
                    SegundoApellido = Texto.LetraCapital(request.SegundoApellido),
                    SexoId = request.SexoId,
                    ParentescoId = request.ParentescoId,
                    Dependiente = request.Dependiente == true,
                    FechaNacimiento = request.FechaNacimiento,
                    TipoDocumentoId = request.TipoDocumentoId,
                    NumeroDocumento = request.NumeroDocumento,
                    NivelEducativoId = request.NivelEducativoId,
                    TelefonoFijo = request.TelefonoFijo,
                    Celular = request.Celular,
                    DivisionPoliticaNivel2Id = request.DivisionPoliticaNivel2Id,
                    Direccion = request.Direccion
                };

                informacionFamiliar.Estado = EstadoInformacionFuncionario.Validado;
                var a = headers[headKeyEsMovil].ToString();
                if (headers[headKeyEsMovil].ToString() != null && headers[headKeyEsMovil].ToString() == secretMovil)
                {
                    informacionFamiliar.Estado = EstadoInformacionFuncionario.Pendiente;
                }
                contexto.InformacionFamiliares.Add(informacionFamiliar);
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
