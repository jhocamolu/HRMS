using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Crear
{
    public class CrearNomenclaturaDianHandler : IRequestHandler<CrearNomenclaturaDianRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearNomenclaturaDianHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNomenclaturaDianRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NomenclaturaDian nomenclaturaDian = new NomenclaturaDian
                {
                    Codigo = request.Codigo.ToUpper(),
                    Nombre = request.Nombre.ToUpper(),
                    TextoPosterior = request.TextoPosterior
                };

                this.contexto.NomenclaturaDians.Add(nomenclaturaDian);
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
