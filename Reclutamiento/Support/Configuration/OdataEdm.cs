using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Support.Configuration
{
    public static class OdataEdm
    {
        public static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EnableLowerCamelCase();
            
            return builder.GetEdmModel();
        }
    }
}
