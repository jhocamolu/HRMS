using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Crear
{
    public class CrearContratoOtroSiHandler : IRequestHandler<CrearContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearContratoOtroSiHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ContratoOtroSi otroSi = new ContratoOtroSi();
                // Consulta id para tipo contrato, termino indefinido
                var tipoContrato = contexto.TipoContratos.FirstOrDefault(x => x.Nombre == "Término indefinido");
                if (request.TipoContratoId == tipoContrato.Id)
                {
                    //Consulta año por defecto
                    var annoVigencia = contexto.AnnoVigencias.FirstOrDefault(x => x.Estado == EstadoAnnoVigencia.Vigente);
                    //Consulta parametro fecha 
                    var consultaFechaFinalizacion = contexto.ParametroGenerales.FirstOrDefault(x => x.Alias == "FechaContratoTerminoIndefinido" &&
                    x.AnnoVigenciaId == annoVigencia.Id);

                    request.FechaFinalizacion = DateTime.Parse(consultaFechaFinalizacion.Valor);
                }
                otroSi.ContratoId = (int)request.ContratoId;
                otroSi.TipoContratoId = (int)request.TipoContratoId;
                otroSi.FechaFinalizacion = request.FechaFinalizacion; 
                otroSi.FechaAplicacion = (DateTime)request.FechaAplicacion;
                otroSi.CargoDependenciaId = (int)request.CargoDependenciaId;
                otroSi.NumeroOtroSi = (int)request.NumeroOtroSi;
                otroSi.Sueldo = (double)request.Sueldo;
                otroSi.CentroOperativoId = (int)request.CentroOperativoId;
                otroSi.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                otroSi.Observaciones = Texto.TipoOracion(request.Observaciones);
                otroSi.Prorroga = (bool)(request.Prorroga == true ? true : false);
                if (request.Prorroga == true)
                {
                    request.numProrogas++;
                    otroSi.NumeroProrroga = request.numProrogas;
                }

                this.contexto.ContratoOtroSis.Add(otroSi);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(otroSi);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
