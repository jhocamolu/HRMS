using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RepresentanteEmpresas.Consultas.Obtener
{
    public class ObtenerRepresentanteEmpresaHandler : IRequestHandler<ObtenerRepresentanteEmpresaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ObtenerRepresentanteEmpresaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerRepresentanteEmpresaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var representante = await contexto.RepresentanteEmpresas
                                            .Include(x => x.Funcionario)
                                            .ThenInclude(x => x.DivisionPoliticaNivel2ExpedicionDocumento)
                                            .ThenInclude(x => x.DivisionPoliticaNivel1)
                                            .ThenInclude(x => x.Pais)
                                            .Include(x => x.Cargo)
                                            .FirstOrDefaultAsync(x =>
                                                          x.GrupoDocumentoSlug == request.GrupoDocumentoSlug &&
                                                          request.FechaInicio >= x.FechaInicio &&
                                                          request.FechaFin <= x.FechaFin);


                return CommandResult.Success(representante);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
