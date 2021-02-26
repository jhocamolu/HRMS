
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoAdministradoras.Comandos.Actualizar
{
    public class ActualizarContratoAdministradoraHandler : IRequestHandler<ActualizarContratoAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarContratoAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarContratoAdministradoraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var contratoAdministradoras = contexto.ContratoAdministradoras.Find(request.Id);
                contratoAdministradoras.AdministradoraId = (int)request.AdministradoraId;
                contratoAdministradoras.Observacion = request.Observacion;
                contratoAdministradoras.FechaInicio = (DateTime)request.FechaInicio;
                contexto.ContratoAdministradoras.Update(contratoAdministradoras);
                await contexto.SaveChangesAsync();


                return CommandResult.Success(contratoAdministradoras);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
