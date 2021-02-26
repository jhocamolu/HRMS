using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Eliminar
{
    /// <summary>
    /// Clase encargada de eliminar registros de  ConceptosNomina
    /// </summary>
    public class EliminarConceptoNominaHandler : IRequestHandler<EliminarConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EliminarConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNomina concepto = await this.contexto.ConceptoNominas.FindAsync(request.Id);

                this.contexto.ConceptoNominas.Remove(concepto);
                await this.contexto.SaveChangesAsync();

                var conceptoTipoAdministradora =  this.contexto.ConceptoNominaTipoAdministradoras
                                                .FirstOrDefault(x=> x.ConceptoNominaId == request.Id &&  
                                                                x.EstadoRegistro == EstadoRegistro.Activo
                );
                if (conceptoTipoAdministradora != null)
                {
                    this.contexto.ConceptoNominaTipoAdministradoras.Remove(conceptoTipoAdministradora);
                    await this.contexto.SaveChangesAsync();
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
