using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones aprciales en el modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Parcial
{
    public class ParcialJornadaLaboralHandler : IRequestHandler<ParcialJornadaLaboralRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialJornadaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialJornadaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var jornadaLaboral = this.contexto.JornadaLaborales.Find(request.Id);
                if (request.Nombre != null) jornadaLaboral.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.Activo != null)
                {
                    jornadaLaboral.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) jornadaLaboral.EstadoRegistro = EstadoRegistro.Inactivo;
                }


                this.contexto.JornadaLaborales.Update(jornadaLaboral);
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
