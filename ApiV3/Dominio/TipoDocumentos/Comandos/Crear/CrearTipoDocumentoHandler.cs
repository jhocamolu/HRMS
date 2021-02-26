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
/// Clase encargada de crear el tipoDocumento, segun el formato 
/// (Alfanumerico O Numerico) se carga un Json de TipoDocumentoFormato.
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Crear
{
    public class CrearTipoDocumentoHandler : IRequestHandler<CrearTipoDocumentoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public CrearTipoDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(CrearTipoDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Validamos el tipo de formato para cargar el array en Formato

                TipoDocumento tipoDocumento = new TipoDocumento
                {
                    CodigoDian = request.CodigoDian,
                    CodigoPila = request.CodigoPila.ToUpper(),
                    Nombre = Texto.TipoOracion(request.Nombre),
                    Formato = request.Formato,
                    Validacion = request.Formato == FormatoValidacion.Numerico ? "^[0-9]*$" : "^[A-Za-z0-9]*$",
                    EquivalenteBancario = request.EquivalenteBancario
                };

                contexto.TipoDocumentos.Add(tipoDocumento);
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