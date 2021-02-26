using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de Actualizar TipoDocumento. 
/// Tener en cuenta que el campo formato solo recive "Alfanumerico" o
/// "Numerico" que se validan con el Emun NombreDocumentoFormato
/// </summary>
/// 
namespace ApiV3.Dominio.TipoDocumentos.Comandos.Actualizar
{
    public class ActualizarTipoDocumentoHandler : IRequestHandler<ActualizarTipoDocumentoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ActualizarTipoDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(ActualizarTipoDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoDocumento tipoDocumento = contexto.TipoDocumentos.Find(request.Id);

                tipoDocumento.CodigoDian = request.CodigoDian;
                tipoDocumento.CodigoPila = request.CodigoPila.ToUpper();
                tipoDocumento.Nombre = Texto.TipoOracion(request.Nombre);
                tipoDocumento.Formato = request.Formato;
                tipoDocumento.Validacion = request.Formato == FormatoValidacion.Numerico ? "^[0-9]*$" : "^[A-Za-z0-9]*$";
                tipoDocumento.EquivalenteBancario = request.EquivalenteBancario;

                contexto.TipoDocumentos.Update(tipoDocumento);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tipoDocumento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}