using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Comandos.Actualizar
{
    public class ActualizarArticuloHandler : IRequestHandler<ActualizarArticuloRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public ActualizarArticuloHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<CommandResult> Handle(ActualizarArticuloRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Articulo articulo = context.Articulos.Find(request.Id);

                articulo.Titulo = request.Titulo;
                articulo.CategoriaId = request.CategoriaId;
                articulo.Orden = (int)request.Orden;
                articulo.Descripcion = request.Descripcion;
                context.Articulos.Update(articulo);
                await context.SaveChangesAsync();

                var articuloId = articulo.Id;
                int eliminarClaves = await context.Database.ExecuteSqlCommandAsync($"DELETE FROM ArticuloClave WHERE ArticuloId={articulo.Id}");
                if (eliminarClaves != 0)
                {
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
                }
                else
                {
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
