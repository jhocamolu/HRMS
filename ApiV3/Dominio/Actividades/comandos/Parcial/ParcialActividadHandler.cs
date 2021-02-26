using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Actividades.comandos.Parcial
{
    public class ParcialActividadHandler : IRequestHandler<ParcialActividadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialActividadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialActividadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Actividad actividad = this.contexto.Actividades.Find(request.Id);
                if (request.Codigo != null)
                {
                    actividad.Codigo = request.Codigo.ToUpper();
                }
                if (request.Nombre != null)
                {
                    actividad.Nombre = request.Nombre;
                }
                if (request.PromedioProductividad != null)
                {
                    actividad.PromedioProductividad = (int)request.PromedioProductividad;
                    actividad.ValorComplejidad = ((decimal)1 / (decimal)request.PromedioProductividad);
                }
                if (request.Descripcion != null)
                {
                    actividad.Descripcion = request.Descripcion;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        actividad.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == false)
                    {
                        actividad.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                this.contexto.Actividades.Update(actividad);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(actividad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
