using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Novedades.Comandos.Actualizar
{
    public class ActualizarNovedadHandler : IRequestHandler<ActualizarNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Novedad novedad = contexto.Novedades.FirstOrDefault(x => x.Id == request.Id);
                novedad.FuncionarioId = (int)request.FuncionarioId;
                novedad.CategoriaNovedadId = (int)request.CategoriaNovedadId;
                novedad.FechaAplicacion = (DateTime)request.FechaAplicacion;
                novedad.FechaFinalizacion = request.FechaFinalizacion;
                novedad.Unidad = (UnidadMedida)request.Unidad;
                novedad.Valor = request.Valor;
                novedad.Cantidad = request.Cantidad;
                novedad.TerceroId = request.TerceroId;
                novedad.Observacion = request.Observacion;

                contexto.Novedades.Update(novedad);
                await contexto.SaveChangesAsync();

                var consulta = contexto.NovedadSubperiodos.Where(x => x.NovedadId == request.Id).ToList();
                foreach (var existen in consulta)
                {
                    if (!request.Periodicidad.Contains(existen.SubperiodoId))
                    {
                        existen.EstadoRegistro = EstadoRegistro.Eliminado;
                        contexto.NovedadSubperiodos.Update(existen);
                        contexto.SaveChanges();
                    }
                }

                foreach (var item in request.Periodicidad)
                {
                    var existe = consulta.FirstOrDefault(x => x.SubperiodoId == item);
                    if (existe == null)
                    {
                        NovedadSubperiodo subperiodo = new NovedadSubperiodo { };
                        subperiodo.NovedadId = novedad.Id;
                        subperiodo.SubperiodoId = item;
                        contexto.NovedadSubperiodos.Add(subperiodo);
                        contexto.SaveChanges();
                    }
                    else
                    {
                        existe.EstadoRegistro = EstadoRegistro.Activo;
                        contexto.NovedadSubperiodos.Update(existe);
                        contexto.SaveChanges();
                    }
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
