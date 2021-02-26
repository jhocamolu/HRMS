using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dependencias.Comandos.Actualizar
{
    public class ActualizarDependenciaHandler : IRequestHandler<ActualizarDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dependencia dependencia = this.contexto.Dependencias.Find(request.Id);

                dependencia.Codigo = request.Codigo;
                dependencia.Nombre = request.Nombre.ToUpper();

                contexto.Dependencias.Update(dependencia);

                await contexto.SaveChangesAsync();

                DependenciaJerarquia dependenciaJerarquia = this.contexto.DependenciaJerarquias
                                                                .FirstOrDefault(x => x.Id == request.DependenciaJerarquiaId);

                dependenciaJerarquia.DependenciaHijoId = request.Id;
                dependenciaJerarquia.DependenciaPadreId = request.DependenciaPadreId;

                contexto.DependenciaJerarquias.Update(dependenciaJerarquia);
                await contexto.SaveChangesAsync();

                if (dependencia.SoyHijoDe != null)
                {
                    foreach (var item in dependencia.SoyHijoDe)
                    {
                        item.DependenciaHijo = null;
                        if (item.DependenciaPadre != null)
                        {
                            item.DependenciaPadre.SoyHijoDe = new List<DependenciaJerarquia>();
                            item.DependenciaPadre.SoyPadreDe = new List<DependenciaJerarquia>();
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
    }
}
