using Ayuda.Dominio.Enumerador;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Parcial
{
    public class ParcialCategoriaHandler : IRequestHandler<ParcialCategoriaRequest, CommandResult>
    {
        private readonly AyudaDbContext contexto;
        public ParcialCategoriaHandler(AyudaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCategoriaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categoria = contexto.Categorias.Find(request.Id);
                if (request.Activo)
                {
                    categoria.EstadoRegistro = EstadoRegistro.Activo;
                }
                else
                {
                    categoria.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                contexto.Categorias.Update(categoria);
                await contexto.SaveChangesAsync();
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
