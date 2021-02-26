using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Parcial
{
    public class ParcialContratoOtroSiHandler : IRequestHandler<ParcialContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialContratoOtroSiHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ContratoOtroSi otroSi = await this.contexto.ContratoOtroSis.FindAsync(request.Id);

                if (request.ContratoId != null) otroSi.ContratoId = (int)request.ContratoId;
                if (request.TipoContratoId != null) otroSi.TipoContratoId = (int)request.TipoContratoId;
                if (request.FechaFinalizacion != null) otroSi.FechaFinalizacion = (DateTime)request.FechaFinalizacion;
                if (request.FechaAplicacion != null) otroSi.FechaAplicacion = (DateTime)request.FechaAplicacion;
                if (request.CargoDependenciaId != null) otroSi.CargoDependenciaId = (int)request.CargoDependenciaId;
                otroSi.NumeroOtroSi = (int)request.NumeroOtroSi;
                if (request.Sueldo != null) otroSi.Sueldo = (double)request.Sueldo;
                if (request.CentroOperativoId != null) otroSi.CentroOperativoId = (int)request.CentroOperativoId;
                if (request.DivisionPoliticaNivel2Id != null) otroSi.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                if (request.Observaciones != null) otroSi.Observaciones = Texto.TipoOracion(request.Observaciones);

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
