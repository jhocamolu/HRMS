using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadFuncionarios.Comandos.Eliminar
{
    public class EliminarActividadFuncionarioHandler : IRequestHandler<EliminarActividadFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarActividadFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarActividadFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var actividadFuncionario = this.contexto.ActividadFuncionarios.Where(x=> x.Estado == EstadoActividadFuncionario.Pendiente &&
                                                                                                          x.EstadoRegistro == EstadoRegistro.Activo)
                                                                                                .ToList();
                contexto.ActividadFuncionarios.RemoveRange(actividadFuncionario);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

       
    }
}
