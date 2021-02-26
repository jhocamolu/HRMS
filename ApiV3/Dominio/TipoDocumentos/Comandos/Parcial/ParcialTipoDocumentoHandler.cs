using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realziar la actualizaciones parciles a la 
/// entidad TipoDocumento
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Estado
{
    public class ParcialTipoDocumentoHandler : IRequestHandler<ParcialTipoDocumentoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialTipoDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoDocumento = this.contexto.TipoDocumentos.Find(request.Id);

                if (request.Activo != null)
                {
                    tipoDocumento.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) tipoDocumento.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                if (request.CodigoDian != null) tipoDocumento.CodigoPila = request.CodigoPila.ToUpper();
                if (request.CodigoDian != null) tipoDocumento.CodigoDian = request.CodigoDian;
                if (request.Nombre != null) tipoDocumento.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.Formato != null)
                {
                    tipoDocumento.Formato = (FormatoValidacion)request.Formato;
                    tipoDocumento.Validacion = request.Formato == FormatoValidacion.Numerico ? "^[0-9]*$" : "^[A-Za-z0-9]*$";
                }
                if (request.EquivalenteBancario != null)
                {
                    tipoDocumento.EquivalenteBancario = request.EquivalenteBancario;
                }


                this.contexto.TipoDocumentos.Update(tipoDocumento);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoDocumento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
