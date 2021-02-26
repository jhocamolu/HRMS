using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ApiV3.Dominio.ContratoOtroSis.Consultas.Documento
{
    public class DocumentoContratoOtroSiHandler : IRequestHandler<DocumentoContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DocumentoContratoOtroSiHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(DocumentoContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(_httpContextAccessor.HttpContext.Request.Headers["JwtToken"], contexto);

                object otroSi = (from o in contexto.ContratoOtroSis.Include(x => x.TipoContrato).Include(x => x.DivisionPoliticaNivel2).ThenInclude(x => x.DivisionPoliticaNivel1).ThenInclude(x => x.Pais)
                                 join c in contexto.Contratos.Include(x => x.TipoContrato).Include(x => x.DivisionPoliticaNivel2).ThenInclude(x => x.DivisionPoliticaNivel1).ThenInclude(x => x.Pais) on o.ContratoId equals c.Id
                                 join f in contexto.Funcionarios on c.FuncionarioId equals f.Id
                                 let otrosiDuracion = Fecha.CalculoDifencia(o.FechaAplicacion, o.FechaFinalizacion.Value)
                                 let informacionBasica = (contexto.InformacionBasicas.First())
                                 let firma = (contexto.RepresentanteEmpresas.OrderByDescending(r => r.FechaInicio).FirstOrDefault(r => r.GrupoDocumentoSlug == "otro-si" &&
                                                               o.FechaAplicacion >= r.FechaInicio &&
                                                               o.FechaAplicacion <= r.FechaFin))
                                 let cargoEmpleador = (contexto.FuncionarioDatoActuales.FirstOrDefault(n => n.Id == f.Id).Cargo.Nombre)
                                 where o.Id == request.OtroSi
                                 select new
                                 {
                                     Tipocontrato = o.TipoContrato.Nombre,
                                     Nombreempleador = informacionBasica.RazonSocial,
                                     Primernombretrabajador = f.PrimerNombre,
                                     Segundonombretrabajador = f.SegundoNombre,
                                     Primerapellidotrabajador = f.PrimerApellido,
                                     Segundoapellidotrabajador = f.SegundoApellido,
                                     Duraciondeotrosiennumero = otrosiDuracion.GetValueOrDefault("enNumero"),
                                     Duraciondeotrosienletras = otrosiDuracion.GetValueOrDefault("enLetras"),
                                     Diadeiniciocontratoennumero = o.FechaAplicacion.Day,
                                     Mesdeiniciocontratoenletras = FechasLetras.ConvertirMes(o.FechaAplicacion.ToString()),
                                     Anioiniciocontratoennumero = o.FechaAplicacion.Year,
                                     Diafinalizacioncontratoennumero = o.FechaFinalizacion.Value.Day,
                                     Mesfinalizacioncontratoenletras = FechasLetras.ConvertirMes(o.FechaFinalizacion.Value.ToString()),
                                     Aniofinalizacioncontratoennumero = o.FechaFinalizacion.Value.Year,
                                     Diafinalizacionotrosiennumero = o.FechaFinalizacion.Value.Day,
                                     Mesfinalizacionotrosienletras = FechasLetras.ConvertirMes(o.FechaFinalizacion.Value.ToString()),
                                     Aniofinalizacionotrosiennumero = o.FechaFinalizacion.Value.Year,
                                     Aniofinalizacionotrosienletras = NumeroLetras.Enletras(o.FechaFinalizacion.Value.Year.ToString()),
                                     Domicilioempleador = informacionBasica.Direccion,
                                     Diafirmaotrosienletras = NumeroLetras.Enletras(DateTime.Now.Day.ToString()),
                                     Diafirmaotrosiennumero = DateTime.Now.Day,
                                     Mesfirmaotrosienletras = FechasLetras.ConvertirMes(DateTime.Now.ToString()),
                                     Aniofirmaotrosienletras = NumeroLetras.Enletras(DateTime.Now.Year.ToString()),
                                     Aniofirmaotrosiennumero = DateTime.Now.Year,
                                     Representanteempleador = firma.Funcionario.PrimerNombre + " " + firma.Funcionario.SegundoNombre + " " + firma.Funcionario.PrimerApellido + " " + firma.Funcionario.SegundoApellido,
                                     Identificacionrepresentanteempleador = firma.Funcionario.NumeroDocumento,
                                     Cargorepresentanteempleador = cargoEmpleador,
                                     Lugarexpediciondocumentoidentidadtrabajador = f.DivisionPoliticaNivel2ExpedicionDocumento.Nombre,
                                     Lugarexpediciondocumentorepresentanteempleador = firma.Funcionario.DivisionPoliticaNivel2ExpedicionDocumento.Nombre,
                                     Diaaemisionennumero = DateTime.Now.Day,
                                     Mesaemisionenletras = FechasLetras.ConvertirMes(DateTime.Now.ToString()),
                                     Anioemisionennumero = DateTime.Now.Year,
                                     Cargo = o.CargoDependencia.Cargo.Nombre,
                                     Sueldo = o.Sueldo,
                                     Diaaplicacionotrosiennumero = o.FechaAplicacion.Day,
                                     Mesaplicacionotrosienletras = FechasLetras.ConvertirMes(o.FechaAplicacion.ToString()),
                                     Anioaplicacionotrosienletras = NumeroLetras.Enletras(o.FechaAplicacion.Year.ToString()),
                                     Anioaplicacionotrosiennumero = o.FechaAplicacion.Year
                                 }).FirstOrDefault()
                             ;


                return CommandResult.Success(otroSi);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
