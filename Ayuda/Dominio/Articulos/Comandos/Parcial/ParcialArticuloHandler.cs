using Ayuda.Dominio.Enumerador;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Comandos.Parcial
{
    public class ParcialArticuloHandler : IRequestHandler<ParcialArticuloRequest, CommandResult>
    {
        private readonly AyudaDbContext contexto;
        public ParcialArticuloHandler(AyudaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialArticuloRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var articulo = contexto.Articulos.Find(request.Id);
                if (request.Activo)
                {
                    articulo.EstadoRegistro = EstadoRegistro.Activo;
                }
                else
                {
                    articulo.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                contexto.Articulos.Update(articulo);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(articulo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }
        }
    }
}
