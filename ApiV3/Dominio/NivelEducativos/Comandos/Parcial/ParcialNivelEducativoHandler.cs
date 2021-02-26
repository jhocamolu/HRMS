using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones parciales a la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Parcial
{
    public class ParcialNivelEducativoHandler : IRequestHandler<ParcialNivelEducativoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialNivelEducativoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialNivelEducativoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var nivelEducativo = this.contexto.NivelEducativos.Find(request.Id);

                if (request.Nombre != null) nivelEducativo.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.Orden != null) nivelEducativo.Orden = (int)request.Orden;
                if (request.Activo != null)
                {
                    nivelEducativo.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) nivelEducativo.EstadoRegistro = EstadoRegistro.Inactivo;
                }

                this.contexto.NivelEducativos.Update(nivelEducativo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(nivelEducativo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
