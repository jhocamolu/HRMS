using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.GrupoNominas.Comandos.Actualizar
{
    public class ActualizarGrupoNominaHandler : IRequestHandler<ActualizarGrupoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarGrupoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarGrupoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GrupoNomina grupoNomina = this.contexto.GrupoNominas.Find(request.Id);
                grupoNomina.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.GrupoNominas.Update(grupoNomina);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(grupoNomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
