using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar validaciones de formato para actualizaciones parciales a la entidad Funcionarios.
/// </summary>

namespace ApiV3.Dominio.Funcionarios.Comandos.Parcial
{
    public class ParcialFuncionarioHandler : IRequestHandler<ParcialFuncionarioRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionario = this.contexto.Funcionarios.Find(request.Id);

                #region Carga de Datos
                #region DATOSBASICOS
                if (request.PrimerNombre != null) funcionario.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                if (request.SegundoNombre != null) funcionario.SegundoNombre = Texto.LetraCapital(request.SegundoNombre);
                if (request.PrimerApellido != null) funcionario.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                if (request.SegundoApellido != null) funcionario.SegundoApellido = Texto.LetraCapital(request.SegundoApellido);
                if (request.SexoId != null) funcionario.SexoId = (int)request.SexoId;
                if (request.EstadoCivilId != null) funcionario.EstadoCivilId = (int)request.EstadoCivilId;
                if (request.Pensionado != null) funcionario.Pensionado = (bool)request.Pensionado;
                if (request.OcupacionId != null) funcionario.OcupacionId = (int)request.OcupacionId;
                #endregion

                #region NACIMINETO
                if (request.FechaNacimiento != null) funcionario.FechaNacimiento = (DateTime)request.FechaNacimiento;
                if (request.DivisionPoliticaNivel2OrigenId != null) funcionario.DivisionPoliticaNivel2OrigenId = (int)request.DivisionPoliticaNivel2OrigenId;
                #endregion

                #region IDENTIFICACION
                if (request.TipoDocumentoId != null) funcionario.TipoDocumentoId = (int)request.TipoDocumentoId;
                if (request.NumeroDocumento != null) funcionario.NumeroDocumento = request.NumeroDocumento;
                if (request.FechaExpedicionDocumento != null) funcionario.FechaExpedicionDocumento = (DateTime)request.FechaExpedicionDocumento;
                if (request.DivisionPoliticaNivel2ExpedicionDocumentoId != null) funcionario.DivisionPoliticaNivel2ExpedicionDocumentoId = (int)request.DivisionPoliticaNivel2ExpedicionDocumentoId;
                if (request.Nit != null) funcionario.Nit = request.Nit;
                if (request.DigitoVerificacion != null) funcionario.DigitoVerificacion = (int)request.DigitoVerificacion;
                #endregion

                #region RESIDENCIA
                if (request.DivisionPoliticaNivel2ResidenciaId != null) funcionario.DivisionPoliticaNivel2ResidenciaId = (int)request.DivisionPoliticaNivel2ResidenciaId;
                if (request.Celular != null) funcionario.Celular = request.Celular;
                if (request.TelefonoFijo != null) funcionario.TelefonoFijo = request.TelefonoFijo;
                if (request.TipoViviendaId != null) funcionario.TipoViviendaId = (int)request.TipoViviendaId;
                if (request.Direccion != null) funcionario.Direccion = request.Direccion;
                #endregion

                #region LIBRETAMILITAR
                if (request.ClaseLibretaMilitarId != null) funcionario.ClaseLibretaMilitarId = (int)request.ClaseLibretaMilitarId;
                if (request.NumeroLibreta != null) funcionario.NumeroLibreta = request.NumeroLibreta;
                if (request.Distrito != null) funcionario.Distrito = request.Distrito;
                #endregion

                #region LICENCIACONDUCCION
                if (request.LicenciaConduccionAId != null) funcionario.LicenciaConduccionAId = request.LicenciaConduccionAId;
                if (request.LicenciaConduccionAFechaVencimiento != null) funcionario.LicenciaConduccionAFechaVencimiento = request.LicenciaConduccionAFechaVencimiento;
                if (request.LicenciaConduccionBId != null) funcionario.LicenciaConduccionBId = request.LicenciaConduccionBId;
                if (request.LicenciaConduccionBFechaVencimiento != null) funcionario.LicenciaConduccionBFechaVencimiento = request.LicenciaConduccionBFechaVencimiento;
                if (request.LicenciaConduccionCId != null) funcionario.LicenciaConduccionCId = request.LicenciaConduccionCId;
                if (request.LicenciaConduccionCFechaVencimiento != null) funcionario.LicenciaConduccionCFechaVencimiento = request.LicenciaConduccionCFechaVencimiento;
                #endregion

                #region OTROS
                if (request.TallaCamisa != null) funcionario.TallaCamisa = request.TallaCamisa.ToUpper();
                if (request.TallaPantalon != null) funcionario.TallaPantalon = request.TallaPantalon;
                if (request.NumeroCalzado != null) funcionario.NumeroCalzado = request.NumeroCalzado;
                if (request.UsaLentes != null) funcionario.UsaLentes = (bool)request.UsaLentes;
                if (request.TipoSangreId != null) funcionario.TipoSangreId = (int)request.TipoSangreId;
                if (request.CorreoElectronicoPersonal != null) funcionario.CorreoElectronicoPersonal = request.CorreoElectronicoPersonal;
                if (request.CorreoElectronicoCorporativo != null) funcionario.CorreoElectronicoCorporativo = request.CorreoElectronicoCorporativo;
                if (request.Adjunto != null) funcionario.Adjunto = request.Adjunto;
                #endregion

                #region Estado_Registro
                if (request.Activo != null)
                {
                    funcionario.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true)
                    {
                        funcionario.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                #endregion
                #endregion

                this.contexto.Funcionarios.Update(funcionario);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(funcionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
