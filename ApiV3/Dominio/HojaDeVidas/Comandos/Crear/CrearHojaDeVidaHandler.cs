using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidas.Comandos.Crear
{
    public class CrearHojaDeVidaHandler : IRequestHandler<CrearHojaDeVidaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearHojaDeVidaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearHojaDeVidaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVida hojaDeVida = new HojaDeVida { };
                hojaDeVida.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                hojaDeVida.SegundoNombre = request.SegundoNombre;
                hojaDeVida.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                hojaDeVida.SegundoApellido = request.SegundoApellido;
                hojaDeVida.SexoId = (int)request.SexoId;
                hojaDeVida.EstadoCivilId = request.EstadoCivilId;
                hojaDeVida.OcupacionId = request.OcupacionId;
                hojaDeVida.Pensionado = request.Pensionado;
                hojaDeVida.FechaNacimiento = request.FechaNacimiento;
                hojaDeVida.DivisionPoliticaNivel2OrigenId = request.DivisionPoliticaNivel2OrigenId;
                hojaDeVida.TipoDocumentoId = (int)request.TipoDocumentoId;
                hojaDeVida.NumeroDocumento = request.NumeroDocumento;
                hojaDeVida.FechaExpedicionDocumento = request.FechaExpedicionDocumento;
                hojaDeVida.DivisionPoliticaNivel2ExpedicionDocumentoId = request.DivisionPoliticaNivel2ExpedicionDocumentoId;
                hojaDeVida.Nit = request.Nit;
                hojaDeVida.DigitoVerificacion = request.DigitoVerificacion;
                hojaDeVida.DivisionPoliticaNivel2ResidenciaId = request.DivisionPoliticaNivel2ResidenciaId;
                hojaDeVida.Celular = request.Celular;
                hojaDeVida.TelefonoFijo = request.TelefonoFijo;
                hojaDeVida.TipoViviendaId = request.TipoViviendaId;
                hojaDeVida.Direccion = request.Direccion;
                hojaDeVida.ClaseLibretaMilitarId = request.ClaseLibretaMilitarId;
                hojaDeVida.NumeroLibreta = request.NumeroLibreta;
                hojaDeVida.Distrito = request.Distrito;
                hojaDeVida.LicenciaConduccionAId = request.LicenciaConduccionAId;
                hojaDeVida.LicenciaConduccionAFechaVencimiento = request.LicenciaConduccionAFechaVencimiento;
                hojaDeVida.LicenciaConduccionBId = request.LicenciaConduccionBId;
                hojaDeVida.LicenciaConduccionBFechaVencimiento = request.LicenciaConduccionBFechaVencimiento;
                hojaDeVida.LicenciaConduccionCId = request.LicenciaConduccionCId;
                hojaDeVida.LicenciaConduccionCFechaVencimiento = request.LicenciaConduccionCFechaVencimiento;
                hojaDeVida.TallaCamisa = request.TallaCamisa;
                hojaDeVida.TallaPantalon = request.TallaPantalon;
                hojaDeVida.NumeroCalzado = request.NumeroCalzado;
                hojaDeVida.UsaLentes = request.UsaLentes;
                hojaDeVida.TipoSangreId = request.TipoSangreId;
                hojaDeVida.CorreoElectronicoPersonal = request.CorreoElectronicoPersonal;


                contexto.HojaDeVidas.Add(hojaDeVida);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(hojaDeVida);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
