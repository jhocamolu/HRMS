using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Actualizar
{
    public class ActualizarHojaDeVidaExperienciaLaboralHandler : IRequestHandler<ActualizarHojaDeVidaExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarHojaDeVidaExperienciaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarHojaDeVidaExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaExperienciaLaboral experiencia = contexto.HojaDeVidaExperienciaLaborales.Find(request.Id);
                experiencia.HojaDeVidaId = request.HojaDeVidaId;
                experiencia.NombreCargo = Texto.TipoOracion(request.NombreCargo);
                experiencia.NombreEmpresa = request.NombreEmpresa;
                experiencia.Telefono = request.Telefono;
                experiencia.Salario = request.Salario;
                experiencia.NombreJefeInmediato = Texto.LetraCapital(request.NombreJefeInmediato);
                experiencia.FechaInicio = (DateTime)request.FechaInicio;
                experiencia.FechaFin = request.FechaFin;
                experiencia.FuncionesCargo = request.FuncionesCargo;
                experiencia.TrabajaActualmente = request.TrabajaActualmente;
                experiencia.MotivoRetiro = request.MotivoRetiro;
                experiencia.Observaciones = request.Observaciones;

                contexto.HojaDeVidaExperienciaLaborales.Update(experiencia);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(experiencia);
            }

            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
