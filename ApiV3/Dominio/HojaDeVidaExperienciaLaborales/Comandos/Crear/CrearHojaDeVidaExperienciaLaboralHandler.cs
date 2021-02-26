using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Crear
{
    public class CrearHojaDeVidaExperienciaLaboralHandler : IRequestHandler<CrearHojaDeVidaExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearHojaDeVidaExperienciaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearHojaDeVidaExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaExperienciaLaboral experiencia = new HojaDeVidaExperienciaLaboral
                {
                    HojaDeVidaId = request.HojaDeVidaId,
                    NombreCargo = Texto.TipoOracion(request.NombreCargo),
                    NombreEmpresa = request.NombreEmpresa,
                    Telefono = request.Telefono,
                    Salario = request.Salario,
                    NombreJefeInmediato = Texto.LetraCapital(request.NombreJefeInmediato),
                    FechaInicio = (DateTime)request.FechaInicio,
                    FechaFin = request.FechaFin,
                    FuncionesCargo = request.FuncionesCargo,
                    TrabajaActualmente = request.TrabajaActualmente,
                    MotivoRetiro = request.MotivoRetiro,
                    Observaciones = request.Observaciones
                };

                this.contexto.HojaDeVidaExperienciaLaborales.Add(experiencia);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(experiencia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
