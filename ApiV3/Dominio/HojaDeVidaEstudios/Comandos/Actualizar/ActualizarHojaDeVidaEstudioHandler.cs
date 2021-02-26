using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Actualizar
{
    public class ActualizarHojaDeVidaEstudioHandler : IRequestHandler<ActualizarHojaDeVidaEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarHojaDeVidaEstudioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarHojaDeVidaEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {


                HojaDeVidaEstudio estudio = await contexto.HojaDeVidaEstudios.FindAsync(request.Id);

                estudio.HojaDeVidaId = (int)request.HojaDeVidaId;
                estudio.NivelEducativoId = (int)request.NivelEducativoId;
                estudio.InstitucionEducativa = request.InstitucionEducativa;
                estudio.PaisId = (int)request.PaisId;
                estudio.ProfesionId = request.ProfesionId;
                estudio.FechaInicio = (DateTime)request.FechaInicio;
                estudio.FechaFin = request.FechaFin;
                estudio.EstadoEstudio = (EstadoEstudio)request.EstadoEstudio;
                if (request.TarjetaProfesional != null)
                {
                    estudio.TarjetaProfesional = request.TarjetaProfesional.ToUpper();
                }

                estudio.Titulo = Texto.TipoOracion(request.Titulo);
                estudio.Observacion = request.Observacion;

                this.contexto.HojaDeVidaEstudios.Update(estudio);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(estudio);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
