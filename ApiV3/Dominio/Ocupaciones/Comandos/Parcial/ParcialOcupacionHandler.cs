using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Ocupaciones.Comandos.Parcial
{
    public class ParcialOcupacionHandler : IRequestHandler<ParcialOcupacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialOcupacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialOcupacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Ocupacion ocupacion = contexto.Ocupaciones.Find(request.Id);

                if (request.Codigo != null)
                {
                    ocupacion.Codigo = request.Codigo;
                }
                if (request.Nombre != null)
                {
                    ocupacion.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                }

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        ocupacion.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        ocupacion.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.Ocupaciones.Update(ocupacion);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(ocupacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
