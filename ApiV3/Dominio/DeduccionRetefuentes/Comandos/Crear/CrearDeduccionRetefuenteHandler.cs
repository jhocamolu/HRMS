using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Crear
{
    public class CrearDeduccionRetefuenteHandler : IRequestHandler<CrearDeduccionRetefuenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearDeduccionRetefuenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDeduccionRetefuenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DeduccionRetefuente deduccionRetefuente = new DeduccionRetefuente { };

                deduccionRetefuente.FuncionarioId = (int)request.FuncionarioId;
                deduccionRetefuente.AnnoVigenciaId = (int)request.AnnoVigenciaId;
                deduccionRetefuente.InteresVivienda = request.InteresVivienda;
                deduccionRetefuente.MedicinaPrepagada = request.MedicinaPrepagada;

                this.contexto.DeduccionRetefuentes.Add(deduccionRetefuente);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(deduccionRetefuente);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
