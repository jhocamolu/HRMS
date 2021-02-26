using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Crear
{
    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public CrearCategoriaHandler(AyudaDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandResult> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    Nombre = request.Nombre,
                    Orden = (int) request.Orden,
                    CategoriaId = request.CategoriaId
                };
                context.Categorias.Add(categoria);
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
