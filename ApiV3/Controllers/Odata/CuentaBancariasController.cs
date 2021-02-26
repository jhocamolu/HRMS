using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador

namespace ApiV3.Controllers.Odata
{
    public class CuentaBancariasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public CuentaBancariasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/CuentaBancarias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<CuentaBancaria> Get()
        {
            return this.contexto.CuentaBancarias;
        }


        //GET: odata/CuentaBancarias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<CuentaBancaria> Get([FromODataUri] int key)
        {
            IQueryable<CuentaBancaria> cuentaBancaria = this.contexto.CuentaBancarias.Where(p => p.Id == key);
            return SingleResult.Create(cuentaBancaria);
        }
    }
}