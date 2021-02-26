using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ParametroGenerales.Comandos.Parcial
{
    public class ParcialParametroGeneralHandler : IRequestHandler<ParcialParametroGeneralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialParametroGeneralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialParametroGeneralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parametroGeneral = contexto.ParametroGenerales.FirstOrDefault(x => x.Alias == request.Alias &&
                                                                                       x.AnnoVigenciaId == request.AnnoVigenciaId);
                parametroGeneral.Valor = request.Valor;

                contexto.ParametroGenerales.Update(parametroGeneral);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(parametroGeneral);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
