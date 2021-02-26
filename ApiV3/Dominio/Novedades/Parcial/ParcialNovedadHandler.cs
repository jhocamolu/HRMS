using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Novedades.Comandos.Parcial
{
    public class ParcialNovedadHandler : IRequestHandler<ParcialNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Novedad novedad = contexto.Novedades.FirstOrDefault(x => x.Id == request.Id);

                if (request.Estado != null)
                {
                    novedad.Estado = (EstadoNovedad)request.Estado;
                }
                if (request.Activo != null)
                {
                    novedad.EstadoRegistro = (bool)request.Activo ? EstadoRegistro.Activo : EstadoRegistro.Inactivo;
                }

                contexto.Novedades.Update(novedad);
                await contexto.SaveChangesAsync();

                if (request.Activo != null)
                {
                    if ((bool)request.Activo)
                    {
                        var consulta = contexto.NovedadSubperiodos.Where(x => x.NovedadId == request.Id).ToList();
                        foreach (var existen in consulta)
                        {
                            existen.EstadoRegistro = EstadoRegistro.Activo;
                            contexto.NovedadSubperiodos.Update(existen);
                            contexto.SaveChanges();
                        }
                    }
                    else
                    {
                        var consulta = contexto.NovedadSubperiodos.Where(x => x.NovedadId == request.Id).ToList();
                        foreach (var existen in consulta)
                        {
                            existen.EstadoRegistro = EstadoRegistro.Inactivo;
                            contexto.NovedadSubperiodos.Update(existen);
                            contexto.SaveChanges();
                        }
                    }

                }


                novedad.NovedadSubperiodos = null;
                return CommandResult.Success(novedad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
