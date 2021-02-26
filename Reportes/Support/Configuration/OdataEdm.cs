using Reportes.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Linq;
using Reportes.Models.NoMapeado;

namespace Reportes.Support.Configuration
{
    public static class OdataEdm
    {

        public static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EnableLowerCamelCase();
            builder.EntitySet<Categoria>("Categorias")
              .EntityType
              .Filter()
              .Count()
              .Expand(30)
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<Subcategoria>("Subcategorias")
              .EntityType
              .Filter()
              .Count()
              .Expand(30)
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<Reporte>("Reportes")
               .EntityType
               .Filter()
               .Count()
               .Expand(30)
               .OrderBy()
               .Page()
               .Select();
            builder.EntitySet<Parametro>("Parametros")
               .EntityType
               .Filter()
               .Count()
               .Expand(30)
               .OrderBy()
               .Page()
               .Select();
            builder.EntitySet<ReporteParametro>("ReporteParametros")
               .EntityType
               .Filter()
               .Count()
               .Expand(30)
               .OrderBy()
               .Page()
               .Select();
            builder.EntitySet<VistaFrontendReporte>("VistaFrontendReportes")
               .EntityType
               .Filter()
               .Count()
               .Expand(30)
               .OrderBy()
               .Page()
               .Select();

            #region Custom 

            //builder.Function("GetNominaFuncionarioDatoActuales")
            //    .Returns<IQueryable<VistaNominaFuncionario>>()
            //    .Parameter<int>("NominaId");

            #endregion

            return builder.GetEdmModel();
        }
    }
}

