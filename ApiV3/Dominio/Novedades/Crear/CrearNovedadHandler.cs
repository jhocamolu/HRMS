using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Novedades.Comandos.Crear
{
    public class CrearNovedadHandler : IRequestHandler<CrearNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Novedad novedad = new Novedad { };

                novedad.FuncionarioId = (int)request.FuncionarioId;
                novedad.CategoriaNovedadId = (int)request.CategoriaNovedadId;
                novedad.FechaAplicacion = (DateTime)request.FechaAplicacion;
                novedad.FechaFinalizacion = request.FechaFinalizacion;
                novedad.Unidad = (UnidadMedida)request.Unidad;
                novedad.Valor = request.Valor;
                novedad.Cantidad = request.Cantidad;
                novedad.TerceroId = request.TerceroId;
                novedad.Observacion = request.Observacion;
                novedad.Estado = EstadoNovedad.Pendiente;
                contexto.Novedades.Add(novedad);
                await contexto.SaveChangesAsync();

                foreach (var item in request.Periodicidad)
                {
                    NovedadSubperiodo subperiodo = new NovedadSubperiodo { };
                    subperiodo.NovedadId = novedad.Id;
                    subperiodo.SubperiodoId = item;
                    contexto.NovedadSubperiodos.Add(subperiodo);
                    contexto.SaveChanges();
                }
                novedad.NovedadSubperiodos = null;
                return CommandResult.Success(novedad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
