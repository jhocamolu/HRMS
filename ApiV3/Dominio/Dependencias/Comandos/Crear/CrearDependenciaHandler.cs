using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dependencias.Comandos.Crear
{
    public class CrearDependenciaHandler : IRequestHandler<CrearDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dependencia dependencia = new Dependencia
                {
                    Codigo = request.Codigo,
                    Nombre = request.Nombre.ToUpper(),
                };


                contexto.Dependencias.Add(dependencia);

                await contexto.SaveChangesAsync();

                DependenciaJerarquia dependenciaJerarquia = new DependenciaJerarquia
                {
                    DependenciaPadreId = request.DependenciaPadreId,
                    DependenciaHijoId = dependencia.Id
                };

                contexto.DependenciaJerarquias.Add(dependenciaJerarquia);
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
