using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.LogConfiguraciones.Comandos
{
    public class ParcialLogConfiguracionHandler : IRequestHandler<ParcialLogConfiguracionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialLogConfiguracionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialLogConfiguracionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var _logConfiguracion = this.contexto._LogConfiguraciones.Find(request.Id);
                _logConfiguracion.Activo = request.Activo;


                this.contexto._LogConfiguraciones.Update(_logConfiguracion);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(_logConfiguracion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
