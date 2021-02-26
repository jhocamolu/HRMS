using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU067_LibroVacaciones
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class LibroVacacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LibroVacacionesController(NominaDbContext context)
        {
            contexto = context;
        }

        //GET: odata/LibroVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacaciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<LibroVacacion> Get()
        {
            return this.contexto.LibroVacaciones.Select(n => new LibroVacacion
            {
                Id = n.Id,
                ContratoId = n.ContratoId,
                FuncionarioId = n.Contrato.FuncionarioId,
                Contrato = n.Contrato,
                InicioCausacion = n.InicioCausacion,
                FinCausacion = n.FinCausacion,
                Tipo = n.Tipo,
                DiasTrabajados = n.DiasTrabajados,
                DiasCausados = n.DiasCausados,
                DiasDisponibles = n.DiasDisponibles,
                DiasDebe = n.DiasDebe
            });
        }

        //GET: odata/LibroVacacion/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacaciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<LibroVacacion> Get([FromODataUri] int key)
        {
            IQueryable<LibroVacacion> libroVacaciones = this.contexto.LibroVacaciones.Select(n => new LibroVacacion
            {
                Id = n.Id,
                ContratoId = n.ContratoId,
                Contrato = n.Contrato,
                FuncionarioId = n.Contrato.FuncionarioId,
                InicioCausacion = n.InicioCausacion,
                FinCausacion = n.FinCausacion,
                Tipo = n.Tipo,
                DiasTrabajados = n.DiasTrabajados,
                DiasCausados = n.DiasCausados,
                DiasDisponibles = n.DiasDisponibles,
                DiasDebe = n.DiasDebe
            }).Where(p => p.Id == key);
            return SingleResult.Create(libroVacaciones);
        }
    }
}
