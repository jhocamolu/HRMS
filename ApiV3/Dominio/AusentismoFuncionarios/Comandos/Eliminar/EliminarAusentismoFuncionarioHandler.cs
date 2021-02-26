using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Eliminar
{
    public class EliminarAusentismoFuncionarioHandler : IRequestHandler<EliminarAusentismoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarAusentismoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarAusentismoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Elimina el registro anteriormente creado
                var prorrogaAusentismos = this.contexto.ProrrogaAusentismos.FirstOrDefault(x => x.ProrrogaId == request.Id && x.EstadoRegistro == EstadoRegistro.Activo);
                if (prorrogaAusentismos != null)
                {
                    return CommandResult.Fail("No se puede borrar registro, se encuentra asociado a una prorroga.", 400);
                }
                else
                {
                    AusentismoFuncionario ausentismoFuncionario = await this.contexto.AusentismoFuncionarios.FindAsync(request.Id);
                    if (ausentismoFuncionario == null)
                    {
                        return CommandResult.Fail("No existe", 404);
                    }
                    this.contexto.AusentismoFuncionarios.Remove(ausentismoFuncionario);
                    await contexto.SaveChangesAsync();

                    var prorroga = this.contexto.ProrrogaAusentismos.Where(x => x.AusentismoId == request.Id).ToList();
                    foreach (var borrarProrroga in prorroga)
                    {
                        this.contexto.ProrrogaAusentismos.Remove(borrarProrroga);
                        await contexto.SaveChangesAsync();
                    }
                }




                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
