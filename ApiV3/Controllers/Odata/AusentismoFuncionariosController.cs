using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU048_Ausentismo_Funcionario
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class AusentismoFuncionariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;


        public AusentismoFuncionariosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/AusentismoFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public IQueryable<AusentismoFuncionario> Get()
        {
            var ausentismoFuncionarios = this.contexto.AusentismoFuncionarios;
            foreach (var item in ausentismoFuncionarios)
            {
                item.ValidaFechaFinal = ((((from nd in contexto.NominaDetalles
                                            join nfn in contexto.NominaFuenteNovedades on nd.NominaFuenteNovedadId equals nfn.Id
                                            join nf in contexto.NominaFuncionarios on nd.NominaFuncionarioId equals nf.Id
                                            join nom in contexto.Nominas on nf.NominaId equals nom.Id
                                            where nfn.Modulo == ModuloSistema.Ausentismos.ToString()
                                            where nfn.ModuloRegistroId == item.Id
                                            where nfn.EstadoRegistro == EstadoRegistro.Activo
                                            where nf.EstadoRegistro == EstadoRegistro.Activo
                                            where nom.EstadoRegistro == EstadoRegistro.Activo
                                            select nom.Id
                ).Count()) > 0) ? true : false);
                item.ValidaTodo = ((((from nd in contexto.NominaDetalles
                                      join nfn in contexto.NominaFuenteNovedades on nd.NominaFuenteNovedadId equals nfn.Id
                                      join nf in contexto.NominaFuncionarios on nd.NominaFuncionarioId equals nf.Id
                                      join nom in contexto.Nominas on nf.NominaId equals nom.Id
                                      where nfn.Modulo == ModuloSistema.Ausentismos.ToString()
                                      where nfn.ModuloRegistroId == item.Id
                                      where nfn.EstadoRegistro == EstadoRegistro.Activo
                                      where nf.EstadoRegistro == EstadoRegistro.Activo
                                      where nom.EstadoRegistro == EstadoRegistro.Activo
                                      select nom.Id
                ).Count()) == 0) ? true : false);
            }
            return ausentismoFuncionarios;

        }



        // GET: odata/AusentismoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<AusentismoFuncionario> Get([FromODataUri] int key)
        {
            IQueryable<AusentismoFuncionario> resultado = this.contexto.AusentismoFuncionarios.Where(p => p.Id == key);
            foreach (var item in resultado)
            {
                item.ValidaFechaFinal = ((((from nd in contexto.NominaDetalles
                                            join nfn in contexto.NominaFuenteNovedades on nd.NominaFuenteNovedadId equals nfn.Id
                                            join nf in contexto.NominaFuncionarios on nd.NominaFuncionarioId equals nf.Id
                                            join nom in contexto.Nominas on nf.NominaId equals nom.Id
                                            where nfn.Modulo == ModuloSistema.Ausentismos.ToString()
                                            where nfn.ModuloRegistroId == item.Id
                                            where nfn.EstadoRegistro == EstadoRegistro.Activo
                                            where nf.EstadoRegistro == EstadoRegistro.Activo
                                            where nom.EstadoRegistro == EstadoRegistro.Activo
                                            select nom.Id
                ).Count()) > 0) ? true : false);
                item.ValidaTodo = ((((from nd in contexto.NominaDetalles
                                      join nfn in contexto.NominaFuenteNovedades on nd.NominaFuenteNovedadId equals nfn.Id
                                      join nf in contexto.NominaFuncionarios on nd.NominaFuncionarioId equals nf.Id
                                      join nom in contexto.Nominas on nf.NominaId equals nom.Id
                                      where nfn.Modulo == ModuloSistema.Ausentismos.ToString()
                                      where nfn.ModuloRegistroId == item.Id
                                      where nfn.EstadoRegistro == EstadoRegistro.Activo
                                      where nf.EstadoRegistro == EstadoRegistro.Activo
                                      where nom.EstadoRegistro == EstadoRegistro.Activo
                                      select nom.Id
                ).Count()) == 0) ? true : false);
            }
            return SingleResult.Create(resultado);
        }
    }
}
