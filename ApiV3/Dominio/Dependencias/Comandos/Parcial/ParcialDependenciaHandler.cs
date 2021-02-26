using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dependencias.Comandos.Parcial
{
    public class ParcialDependenciaHandler : IRequestHandler<ParcialDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dependencia dependencia = this.contexto.Dependencias.Find(request.Id);
                if (request.Activo != null)
                {
                    var estadoRegistro = (bool)request.Activo ? EstadoRegistro.Activo : EstadoRegistro.Inactivo;
                    dependencia.EstadoRegistro = estadoRegistro;
                }
                this.contexto.Update(dependencia);
                this.contexto.SaveChanges();

                DependenciaJerarquia dependenciaJerarquia = this.contexto.DependenciaJerarquias
                                                               .FirstOrDefault(x => x.DependenciaHijoId == dependencia.Id);
                if (request.Activo != null)
                {
                    var estadoRegistro = (bool)request.Activo ? EstadoRegistro.Activo : EstadoRegistro.Inactivo;
                    dependenciaJerarquia.EstadoRegistro = estadoRegistro;
                }

                contexto.DependenciaJerarquias.Update(dependenciaJerarquia);
                await contexto.SaveChangesAsync();

                dependencia.SoyHijoDe = this.contexto.DependenciaJerarquias
                                                           .Where(x => x.DependenciaHijoId == request.Id)
                                                           .Include(x => x.DependenciaPadre)
                                                           .ToList();

                if (dependencia.SoyHijoDe != null)
                {
                    foreach (var item2 in dependencia.SoyHijoDe)
                    {
                        item2.DependenciaHijo = null;
                        if (item2.DependenciaPadre != null)
                        {
                            item2.DependenciaPadre.SoyHijoDe = new List<DependenciaJerarquia>();
                            item2.DependenciaPadre.SoyPadreDe = new List<DependenciaJerarquia>();
                        }
                    }
                }


                if (dependencia.SoyPadreDe != null)
                {
                    foreach (var item in dependencia.SoyPadreDe)
                    {
                        item.DependenciaPadre = null;
                        if (item.DependenciaHijo != null)
                        {
                            item.DependenciaHijo.SoyHijoDe = new List<DependenciaJerarquia>();
                            item.DependenciaHijo.SoyPadreDe = new List<DependenciaJerarquia>();
                        }
                    }
                }


                return CommandResult.Success(dependencia);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }

        //private void cambiarEstado(int id, bool activo)
        //{
        //    try
        //    {
        //        var estadoRegistro = activo ? EstadoRegistro.Activo : EstadoRegistro.Inactivo;

        //        Dependencia dependencia = this.contexto.Dependencias.Find(id);
        //        dependencia.EstadoRegistro = estadoRegistro;
        //        this.contexto.Update(dependencia);

        //        var jerarquias = this.contexto.DependenciaJerarquias
        //                                                    .Where(x => x.DependenciaPadreId == id)
        //                                                    .ToList();
        //        foreach (var item in jerarquias)
        //        {
        //            item.EstadoRegistro = estadoRegistro;
        //            this.contexto.Update(item);
        //            this.cambiarEstado(item.DependenciaHijoId, activo);
        //        }
        //        this.contexto.SaveChanges();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

    }
}
