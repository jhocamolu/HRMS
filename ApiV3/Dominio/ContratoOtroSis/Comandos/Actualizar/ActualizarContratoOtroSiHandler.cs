using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Actualizar
{
    public class ActualizarContratoOtroSiHandler : IRequestHandler<ActualizarContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarContratoOtroSiHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ContratoOtroSi otroSi = await this.contexto.ContratoOtroSis.FindAsync(request.Id);

                otroSi.ContratoId = (int)request.ContratoId;
                otroSi.TipoContratoId = (int)request.TipoContratoId;
                otroSi.FechaFinalizacion = (DateTime)request.FechaFinalizacion;
                otroSi.FechaAplicacion = (DateTime)request.FechaAplicacion;
                otroSi.CargoDependenciaId = (int)request.CargoDependenciaId;
                otroSi.Sueldo = (double)request.Sueldo;
                otroSi.CentroOperativoId = (int)request.CentroOperativoId;
                otroSi.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                otroSi.Observaciones = Texto.TipoOracion(request.Observaciones);

                this.contexto.ContratoOtroSis.Update(otroSi);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(otroSi);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
