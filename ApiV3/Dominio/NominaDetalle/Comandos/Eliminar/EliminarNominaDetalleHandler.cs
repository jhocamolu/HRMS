using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaDetalle.Comandos.Eliminar
{
    public class EliminarNominaDetalleHandler : IRequestHandler<EliminarNominaDetalleRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNominaDetalleHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNominaDetalleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var nominaDetalle = this.contexto.NominaDetalles.FirstOrDefault(x => x.Id == request.Id);
                if (nominaDetalle != null)
                {
                    // Elimina el registro de nómina detalle
                    this.contexto.Database
                                 .ExecuteSqlRaw($"DELETE FROM {typeof(ApiV3.Models.NominaDetalle).Name} WHERE Id ={ request.Id}");

                    if (nominaDetalle.NominaFuenteNovedadId != null)
                    {
                        //Elimina registros de nómina fuente novedad.
                        var validaNominaFuenteNovedad = this.contexto.NominaFuenteNovedades.FirstOrDefault(x => x.Id == nominaDetalle.NominaFuenteNovedadId);

                        if (validaNominaFuenteNovedad != null)
                        {
                            this.contexto.Database
                                         .ExecuteSqlRaw($"DELETE FROM {typeof(NominaFuenteNovedad).Name} WHERE Id ={ validaNominaFuenteNovedad.Id}");
                        }
                    }
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
                    

                    return CommandResult.Success();
                }
                else
                {
                    return CommandResult.Fail("No existe", 404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
