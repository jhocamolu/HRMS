using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Eliminar
{
    public class EliminarNomenclaturaDianHandler : IRequestHandler<EliminarNomenclaturaDianRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNomenclaturaDianHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNomenclaturaDianRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NomenclaturaDian nomenclaturaDian = this.contexto.NomenclaturaDians.Find(request.Id);
                this.contexto.NomenclaturaDians.Remove(nomenclaturaDian);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(nomenclaturaDian);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
