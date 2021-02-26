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

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Parcial
{
    public class ParcialInformacionFamiliarHandler : IRequestHandler<ParcialInformacionFamiliarRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ParcialInformacionFamiliarHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ParcialInformacionFamiliarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                InformacionFamiliar informacionFamiliar = contexto.InformacionFamiliares.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        informacionFamiliar.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        informacionFamiliar.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.PrimerNombre != null)
                {
                    informacionFamiliar.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                }
                if (request.SegundoNombre != null)
                {
                    informacionFamiliar.SegundoNombre = Texto.LetraCapital(request.SegundoNombre);
                }
                if (request.PrimerApellido != null)
                {
                    informacionFamiliar.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                }
                if (request.SegundoApellido != null)
                {
                    informacionFamiliar.SegundoApellido = Texto.LetraCapital(request.SegundoApellido);
                }
                if (request.SexoId != null)
                {
                    informacionFamiliar.SexoId = (int)request.SexoId;
                }
                if (request.ParentescoId != null)
                {
                    informacionFamiliar.ParentescoId = (int)request.ParentescoId;
                }
                if (request.Dependiente != null)
                {
                    informacionFamiliar.Dependiente = (bool)request.Dependiente;
                }
                if (request.FechaNacimiento != null)
                {
                    informacionFamiliar.FechaNacimiento = (DateTime)request.FechaNacimiento;
                }
                if (request.TipoDocumentoId != null)
                {
                    informacionFamiliar.TipoDocumentoId = (int)request.TipoDocumentoId;
                }
                if (request.NumeroDocumento != null)
                {
                    informacionFamiliar.NumeroDocumento = request.NumeroDocumento;
                }
                if (request.NivelEducativoId != null)
                {
                    informacionFamiliar.NivelEducativoId = (int)request.NivelEducativoId;
                }
                if (request.TelefonoFijo != null)
                {
                    informacionFamiliar.TelefonoFijo = request.TelefonoFijo;
                }
                if (request.Celular != null)
                {
                    informacionFamiliar.Celular = request.Celular;
                }
                if (request.DivisionPoliticaNivel2Id != null)
                {
                    informacionFamiliar.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                }
                if (request.Direccion != null)
                {
                    informacionFamiliar.Direccion = request.Direccion;
                }
                if (request.Justificacion != null)
                {
                    informacionFamiliar.Justificacion = request.Justificacion;
                }
                if (request.Estado != null)
                {
                    if (headers[headKeyEsMovil].ToString() == "" || headers[headKeyEsMovil].ToString() == null)
                    {
                        informacionFamiliar.Estado = (EstadoInformacionFuncionario)request.Estado;
                    }
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
