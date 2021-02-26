using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.GrupoNominas.Comandos.Parcial
{
    public class ParcialGrupoNominaHandler : IRequestHandler<ParcialGrupoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialGrupoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialGrupoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GrupoNomina grupoNomina = this.contexto.GrupoNominas.Find(request.Id);
                if (!String.IsNullOrEmpty(request.Nombre))
                {
                    grupoNomina.Nombre = Texto.TipoOracion(request.Nombre);
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        grupoNomina.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        grupoNomina.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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
