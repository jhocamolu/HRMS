using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Parcial
{
    public class ParcialNomenclaturaDianHandler : IRequestHandler<ParcialNomenclaturaDianRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialNomenclaturaDianHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialNomenclaturaDianRequest request, CancellationToken cancellationToken)
        {
            NomenclaturaDian nomenclaturaDian = this.contexto.NomenclaturaDians.Find(request.Id);

            if (request.Activo != null)
            {
                if (request.Activo == true)
                {
                    nomenclaturaDian.EstadoRegistro = EstadoRegistro.Activo;
                }
                else
                {
                    nomenclaturaDian.EstadoRegistro = EstadoRegistro.Inactivo;
                }
            }

            this.contexto.NomenclaturaDians.Update(nomenclaturaDian);
            await this.contexto.SaveChangesAsync();
            return CommandResult.Success(nomenclaturaDian);
        }
    }
}
