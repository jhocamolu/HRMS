using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidas.Comandos.Actualizar
{
    public class ActualizarHojaDeVidaHandler : IRequestHandler<ActualizarHojaDeVidaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarHojaDeVidaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarHojaDeVidaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVida InformacionHojaDeVida = contexto.HojaDeVidas.Find(request.Id);

                #region Carga de Datos
                InformacionHojaDeVida.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                InformacionHojaDeVida.SegundoNombre = request.SegundoNombre != null ? Texto.LetraCapital(request.SegundoNombre) : null;
                InformacionHojaDeVida.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                InformacionHojaDeVida.SegundoApellido = request.SegundoApellido != null ? Texto.LetraCapital(request.SegundoApellido) : null;
                InformacionHojaDeVida.SexoId = (int)request.SexoId;
                InformacionHojaDeVida.OcupacionId = (int)request.OcupacionId;
                InformacionHojaDeVida.EstadoCivilId = (int)request.EstadoCivilId;
                InformacionHojaDeVida.Pensionado = (bool)request.Pensionado;
                InformacionHojaDeVida.FechaNacimiento = (DateTime)request.FechaNacimiento;
                InformacionHojaDeVida.DivisionPoliticaNivel2OrigenId = (int)request.DivisionPoliticaNivel2OrigenId;
                InformacionHojaDeVida.TipoDocumentoId = (int)request.TipoDocumentoId;
                InformacionHojaDeVida.NumeroDocumento = request.NumeroDocumento;
                InformacionHojaDeVida.FechaExpedicionDocumento = (DateTime)request.FechaExpedicionDocumento;
                InformacionHojaDeVida.DivisionPoliticaNivel2ExpedicionDocumentoId = (int)request.DivisionPoliticaNivel2ExpedicionDocumentoId;
                InformacionHojaDeVida.Nit = request.Nit;
                InformacionHojaDeVida.DigitoVerificacion = (int)request.DigitoVerificacion;
                InformacionHojaDeVida.DivisionPoliticaNivel2ResidenciaId = (int)request.DivisionPoliticaNivel2ResidenciaId;
                InformacionHojaDeVida.Celular = request.Celular;
                InformacionHojaDeVida.TelefonoFijo = request.TelefonoFijo;
                InformacionHojaDeVida.TipoViviendaId = (int)request.TipoViviendaId;
                InformacionHojaDeVida.Direccion = request.Direccion;
                InformacionHojaDeVida.ClaseLibretaMilitarId = request.ClaseLibretaMilitarId;
                InformacionHojaDeVida.NumeroLibreta = request.NumeroLibreta;
                InformacionHojaDeVida.Distrito = request.Distrito;
                if (request.LicenciaConduccionAId != null) InformacionHojaDeVida.LicenciaConduccionAId = request.LicenciaConduccionAId;
                if (request.LicenciaConduccionAFechaVencimiento != null) InformacionHojaDeVida.LicenciaConduccionAFechaVencimiento = request.LicenciaConduccionAFechaVencimiento;
                if (request.LicenciaConduccionBId != null) InformacionHojaDeVida.LicenciaConduccionBId = request.LicenciaConduccionBId;
                if (request.LicenciaConduccionBFechaVencimiento != null) InformacionHojaDeVida.LicenciaConduccionBFechaVencimiento = request.LicenciaConduccionBFechaVencimiento;
                if (request.LicenciaConduccionCId != null) InformacionHojaDeVida.LicenciaConduccionCId = request.LicenciaConduccionCId;
                if (request.LicenciaConduccionCFechaVencimiento != null) InformacionHojaDeVida.LicenciaConduccionCFechaVencimiento = request.LicenciaConduccionCFechaVencimiento;
                InformacionHojaDeVida.TallaCamisa = request.TallaCamisa != null ? request.TallaCamisa.ToUpper() : null;
                InformacionHojaDeVida.TallaPantalon = request.TallaPantalon;
                InformacionHojaDeVida.NumeroCalzado = request.NumeroCalzado;
                InformacionHojaDeVida.UsaLentes = (bool)request.UsaLentes;
                InformacionHojaDeVida.TipoSangreId = (int)request.TipoSangreId;
                InformacionHojaDeVida.CorreoElectronicoPersonal = request.CorreoElectronicoPersonal;
                if (request.Adjunto != null) InformacionHojaDeVida.Adjunto = request.Adjunto;
                #endregion

                contexto.HojaDeVidas.Update(InformacionHojaDeVida);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(InformacionHojaDeVida);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
