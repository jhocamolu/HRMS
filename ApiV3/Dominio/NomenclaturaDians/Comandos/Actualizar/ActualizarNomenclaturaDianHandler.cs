using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Actualizar
{
    public class ActualizarNomenclaturaDianHandler : IRequestHandler<ActualizarNomenclaturaDianRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarNomenclaturaDianHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNomenclaturaDianRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NomenclaturaDian nomenclaturaDian = this.contexto.NomenclaturaDians.Find(request.Id);

                nomenclaturaDian.Codigo = request.Codigo;
                nomenclaturaDian.Nombre = request.Nombre;
                nomenclaturaDian.TextoPosterior = request.TextoPosterior;

                this.contexto.NomenclaturaDians.Update(nomenclaturaDian);
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
