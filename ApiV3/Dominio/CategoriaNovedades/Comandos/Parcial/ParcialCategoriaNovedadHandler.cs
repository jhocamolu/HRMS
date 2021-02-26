using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Parcial
{
    public class ParcialCategoriaNovedadHandler : IRequestHandler<ParcialCategoriaNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCategoriaNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCategoriaNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CategoriaNovedad categoriaNovedad = contexto.CategoriaNovedades.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        categoriaNovedad.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        categoriaNovedad.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                this.contexto.CategoriaNovedades.Update(categoriaNovedad);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(categoriaNovedad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
