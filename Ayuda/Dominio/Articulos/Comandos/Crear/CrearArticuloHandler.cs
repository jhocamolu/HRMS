using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Comandos.Crear
{
    public class CrearArticuloHandler : IRequestHandler<CrearArticuloRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public CrearArticuloHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<CommandResult> Handle(CrearArticuloRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Articulo articulo = new Articulo
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    Orden = (int)request.Orden,
                    CategoriaId = request.CategoriaId
                };

                context.Articulos.Add(articulo);
                await context.SaveChangesAsync();

                var articuloId = articulo.Id;

                if (request.Palabras != null)
                {
                    foreach (var item in request.Palabras)
                    {
                        var buscar = (from clave in context.Claves
                                      where clave.Palabra == item
                                      select new
                                      {
                                          clave.Id
                                      }).FirstOrDefault();
                        if (buscar == null)
                        {
                            Clave clave = new Clave
                            {
                                Palabra = item
                            };
                            context.Claves.Add(clave);
                            await context.SaveChangesAsync();
                            var claveid = clave.Id;
                            ArticuloClave articuloClave = new ArticuloClave
                            {
                                ArticuloId = articuloId,
                                ClaveId = claveid
                            };
                            context.ArticuloClaves.Add(articuloClave);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            ArticuloClave articuloClave = new ArticuloClave
                            {
                                ArticuloId = articuloId,
                                ClaveId = buscar.Id
                            };
                            context.ArticuloClaves.Add(articuloClave);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                articulo = await context.Articulos.Include(a => a.ArticuloClaves)
                .ThenInclude(ac => ac.Clave)
                .FirstOrDefaultAsync(a => a.Id == articuloId);
                return CommandResult.Success(articulo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}

