using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models.NoMapeado;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{

    public class CustomController : ODataController
    {
        private readonly NominaDbContext contexto;
        public CustomController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/GetNominaFuncionarioDatoActuales(NominaId=4)
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Custom_ObtenerNominaFuncionarioDatoActuales })]
        [HttpGet]
        [ODataRoute("GetNominaFuncionarioDatoActuales(NominaId={nominaid})")]
        [EnableQuery()]
        public IQueryable<VistaNominaFuncionario> GetNominaFuncionarioDatoActuales([FromODataUri] int nominaid)
        {

            IQueryable<VistaNominaFuncionario> query = from nf in contexto.NominaFuncionarios
                                                       join fda in contexto.FuncionarioDatoActuales
                                                       .Include(x => x.Cargo)
                                                       .Include(x => x.Dependencia)
                                                       .Include(x => x.CentroOperativo)
                                                       .Include(x => x.GrupoNomina)
                                                       on nf.FuncionarioId equals fda.Id
                                                       where nf.NominaId == nominaid
                                                       select new VistaNominaFuncionario
                                                       {
                                                           Id = fda.Id,
                                                           NominaFuncionarioId = nf.Id,
                                                           EstadoNominaFuncionario = nf.Estado,
                                                           CriterioBusqueda = fda.CriterioBusqueda,
                                                           PrimerNombre = fda.PrimerNombre,
                                                           SegundoNombre = fda.SegundoNombre,
                                                           PrimerApellido = fda.PrimerApellido,
                                                           SegundoApellido = fda.SegundoApellido,
                                                           TipoDocumentoId = fda.TipoDocumentoId,
                                                           NumeroDocumento = fda.NumeroDocumento,
                                                           Nit = fda.Nit,
                                                           DigitoVerificacion = fda.DigitoVerificacion,
                                                           Sueldo = fda.Sueldo,
                                                           CargoId = fda.CargoId,
                                                           DependenciaId = fda.DependenciaId,
                                                           CentroOperativoId = fda.CentroOperativoId,
                                                           GrupoNominaId = fda.GrupoNominaId,
                                                           NominaId = nf.NominaId,
                                                           NetoPagar = nf.NetoPagar,
                                                           TipoDocumentoNombre = fda.TipoDocumento.Nombre,
                                                           CargoNombre = fda.Cargo.Nombre,
                                                           DependenciaNombre = fda.Dependencia.Nombre,
                                                           CentroOperativoNombre = fda.CentroOperativo.Nombre,
                                                           GrupoNominaNombre = fda.GrupoNomina.Nombre,
                                                           EstadoFuncionario = nf.Funcionario.Estado.ToString()
                                                       };

            return query;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Custom_ObtenerNominaValorTotal })]
        [HttpGet]
        [ODataRoute("GetNominaValorTotal(FechaInicial={fechaInicial},FechaFinal={fechaFinal})")]
        [EnableQuery()]
        public IQueryable<VistaNominaValor> GetNominaValorTotal([FromODataUri] DateTime fechaInicial, [FromODataUri] DateTime fechaFinal)
        {
            IQueryable<VistaNominaValor> informacion =
                from n in contexto.Nominas.Include(x => x.TipoLiquidacion).Include(x => x.SubPeriodo)
                let valor = contexto.NominaFuncionarios.Where(s => s.NominaId == n.Id).Sum(s => s.NetoPagar)
                where n.FechaInicio.Date >= fechaInicial && n.FechaFinal.Date <= fechaFinal.Date && n.Estado == EstadoNomina.Aplicada
                select new VistaNominaValor
                {
                    Id = n.Id,
                    TipoLiquidacion = n.TipoLiquidacion.Nombre,
                    Subperiodo = n.SubPeriodo.Nombre,
                    FechaInicial = n.FechaInicio,
                    FechaFinal = n.FechaFinal,
                    ValorTotal = valor
                };
            return informacion;
        }
    }
}