using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description HU059-Libranzas
/// Controlador Odata para busqueda HojaDeVidasController

namespace ApiV3.Controllers.Odata
{
    public class HojaDeVidasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public HojaDeVidasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        //GET: odata/HojaDeVidas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<HojaDeVida>> Get()
        {
            return this.contexto.HojaDeVidas;


        }


        //GET: odata/HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<HojaDeVida> Get([FromODataUri] int key)
        {
            IQueryable<HojaDeVida> resultado = this.contexto.HojaDeVidas.Where(p => p.Id == key);

            return SingleResult.Create(resultado);
        }
    }
}
