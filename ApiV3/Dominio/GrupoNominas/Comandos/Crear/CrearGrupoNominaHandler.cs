using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.GrupoNominas.Comandos.Crear
{
    public class CrearGrupoNominaHandler : IRequestHandler<CrearGrupoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearGrupoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearGrupoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GrupoNomina grupoNomina = new GrupoNomina();
                grupoNomina.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.GrupoNominas.Add(grupoNomina);
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
