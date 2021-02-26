using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear registros en el modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Crear
{
    public class CrearJornadaLaboralHandler : IRequestHandler<CrearJornadaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearJornadaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearJornadaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboral jornadaLaboral = new JornadaLaboral
                {
                    Nombre = Texto.TipoOracion(request.Nombre)
                };

                this.contexto.JornadaLaborales.Add(jornadaLaboral);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(jornadaLaboral);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
