using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Aumentar
{
    public class AumentarContratoOtroSiHandler : IRequestHandler<AumentarContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repository;
        public AumentarContratoOtroSiHandler(NominaDbContext contexto, IReadOnlyRepository repository)
        {
            this.contexto = contexto;
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AumentarContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string specifier;
                var aplicar = (request.Aplicar.Value.ToString() == AplicacionAumentarSalario.Todos.ToString()) ? "null" : "'" + request.Aplicar.Value.ToString() + "'";
                specifier = "G";
                var porcentaje = request.PorcentajeAplicacion.Value.ToString(specifier, CultureInfo.InvariantCulture);

                Console.WriteLine(request.FechaAplicacion.ToString(format:"yyyy-MM-dd"));
                Console.WriteLine(porcentaje);

                string query = $@"
                        DECLARE @FechaAplicacion date = '{(request.FechaAplicacion.ToString(format: "yyyy-MM-dd"))}'
                        DECLARE @PorcentajeAplicacion decimal(16,6) = '{porcentaje}'
                        DECLARE @Aplicar varchar(255) = {aplicar}
                        DECLARE @Resultado int

                        EXECUTE [dbo].[USP_IncrementarSalarioFuncionario] 
                           @FechaAplicacion
                          ,@PorcentajeAplicacion
                          ,@Aplicar
                          ,@Resultado OUTPUT";
                try
                {
                    this.repository.NonQuery(query);
                }
                catch (Exception e)
                {
                    return CommandResult.Fail(e.Message, 400);
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
