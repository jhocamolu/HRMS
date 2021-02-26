using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria Rubio
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU099

namespace ApiV3.Controllers.Odata
{
    public class HojaDeVidaEstudiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public HojaDeVidaEstudiosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/HojaDeVidaEstudios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<HojaDeVidaEstudio> Get()
        {
            return this.contexto.HojaDeVidaEstudios;
        }

        // GET: odata/HojaDeVidaEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<HojaDeVidaEstudio> Get([FromODataUri] int key)
        {
            IQueryable<HojaDeVidaEstudio> result = this.contexto.HojaDeVidaEstudios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
