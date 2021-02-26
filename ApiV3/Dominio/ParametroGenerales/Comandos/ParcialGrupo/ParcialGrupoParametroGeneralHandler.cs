using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ParametroGenerales.Comandos.ParcialGrupo
{
    public class ParcialGrupoParametroGeneralHandler : IRequestHandler<ParcialGrupoParametroGeneralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialGrupoParametroGeneralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialGrupoParametroGeneralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.valores)
                {
                    var parametroGeneral = contexto.ParametroGenerales.FirstOrDefault(x => x.Alias == item.Alias && x.AnnoVigenciaId == item.AnnoVigenciaId);

                    parametroGeneral.Valor = item.Valor;

                    contexto.ParametroGenerales.Update(parametroGeneral);
                }
                try
                {
                    await contexto.SaveChangesAsync();
                }
                catch (Exception ex2)
                {
                    return CommandResult.Fail(ex2.Message);
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
