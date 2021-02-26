using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Actualizar
{
    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public ActualizarCategoriaHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<CommandResult> Handle(ActualizarCategoriaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Categoria categoria = context.Categorias.Find(request.Id);

                categoria.Nombre = request.Nombre;
                categoria.Orden = (int) request.Orden;
                categoria.CategoriaId = request.CategoriaId;
                //categoria.Activo = request.Activo;

                context.Categorias.Update(categoria);
                await context.SaveChangesAsync();

                if (categoria.Padre != null)
                {
                    categoria.Padre.Categorias = new List<Categoria>();
                }
                return CommandResult.Success(categoria);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
