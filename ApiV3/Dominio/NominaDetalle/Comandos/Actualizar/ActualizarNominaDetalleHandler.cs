using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaDetalle.Comandos.Actualizar
{
    public class ActualizarNominaDetalleHandler : IRequestHandler<ActualizarNominaDetalleRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarNominaDetalleHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNominaDetalleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var nominaDetalle = contexto.NominaDetalles.Find(request.Id);
                if (request.Cantidad != null)
                {
                    nominaDetalle.Cantidad = (double)request.Cantidad;
                }
                if (request.Valor != null)
                {
                    nominaDetalle.Valor = (double)request.Valor;
                }
                nominaDetalle.Observacion = request.Observacion;
                nominaDetalle.Estado = EstadoNominaDetalle.Pendiente;

                contexto.NominaDetalles.Update(nominaDetalle);
                await contexto.SaveChangesAsync();

                // Actualiza el estado de nomina funcionario
                var nominaFuncionario = contexto.NominaFuncionarios.Find(nominaDetalle.NominaFuncionarioId);
                if (nominaFuncionario != null)
                {
                    nominaFuncionario.Estado = EstadoNominaFuncionario.Asignado;
                    this.contexto.NominaFuncionarios.Update(nominaFuncionario);
                    await contexto.SaveChangesAsync();
                }

                var nomina = contexto.Nominas.Find(nominaFuncionario.NominaId);
                if (nomina != null)
                {
                    nomina.Estado = EstadoNomina.Modificada;
                    this.contexto.Nominas.Update(nomina);
                    await contexto.SaveChangesAsync();
                }

                return CommandResult.Success(nominaDetalle);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}