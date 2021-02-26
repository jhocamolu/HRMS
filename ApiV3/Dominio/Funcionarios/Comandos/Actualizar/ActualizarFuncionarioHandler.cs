using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de actualziar la entidad Funcionarios
/// </summary>
/// 
namespace ApiV3.Dominio.Funcionarios.Comandos.Actualizar
{
    public class ActualizarFuncionarioHandler : IRequestHandler<ActualizarFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Funcionario funcionario = this.contexto.Funcionarios.Find(request.Id);

                #region Carga de Datos
                funcionario.PrimerNombre = Texto.LetraCapital(request.PrimerNombre);
                funcionario.SegundoNombre = request.SegundoNombre != null ? Texto.LetraCapital(request.SegundoNombre) : null;
                funcionario.PrimerApellido = Texto.LetraCapital(request.PrimerApellido);
                funcionario.SegundoApellido = request.SegundoApellido != null ? Texto.LetraCapital(request.SegundoApellido) : null;
                funcionario.SexoId = (int)request.SexoId;
                funcionario.EstadoCivilId = (int)request.EstadoCivilId;
                funcionario.OcupacionId = (int)request.OcupacionId;
                funcionario.Pensionado = (bool)request.Pensionado;
                //EstadoEmpleadoId  Se actualiza desde el contrato
                funcionario.FechaNacimiento = (DateTime)request.FechaNacimiento;
                funcionario.DivisionPoliticaNivel2OrigenId = (int)request.DivisionPoliticaNivel2OrigenId;
                funcionario.TipoDocumentoId = (int)request.TipoDocumentoId;
                funcionario.NumeroDocumento = request.NumeroDocumento;
                funcionario.FechaExpedicionDocumento = (DateTime)request.FechaExpedicionDocumento;
                funcionario.DivisionPoliticaNivel2ExpedicionDocumentoId = (int)request.DivisionPoliticaNivel2ExpedicionDocumentoId;
                funcionario.Nit = request.Nit;
                funcionario.DigitoVerificacion = (int)request.DigitoVerificacion;
                funcionario.DivisionPoliticaNivel2ResidenciaId = (int)request.DivisionPoliticaNivel2ResidenciaId;
                funcionario.Celular = request.Celular;
                funcionario.TelefonoFijo = request.TelefonoFijo;
                funcionario.TipoViviendaId = (int)request.TipoViviendaId;
                funcionario.Direccion = request.Direccion;
                funcionario.ClaseLibretaMilitarId = request.ClaseLibretaMilitarId;
                funcionario.NumeroLibreta = request.NumeroLibreta;
                funcionario.Distrito = request.Distrito;
                if (request.LicenciaConduccionAId != null) funcionario.LicenciaConduccionAId = request.LicenciaConduccionAId;
                if (request.LicenciaConduccionAFechaVencimiento != null) funcionario.LicenciaConduccionAFechaVencimiento = request.LicenciaConduccionAFechaVencimiento;
                if (request.LicenciaConduccionBId != null) funcionario.LicenciaConduccionBId = request.LicenciaConduccionBId;
                if (request.LicenciaConduccionBFechaVencimiento != null) funcionario.LicenciaConduccionBFechaVencimiento = request.LicenciaConduccionBFechaVencimiento;
                if (request.LicenciaConduccionCId != null) funcionario.LicenciaConduccionCId = request.LicenciaConduccionCId;
                if (request.LicenciaConduccionCFechaVencimiento != null) funcionario.LicenciaConduccionCFechaVencimiento = request.LicenciaConduccionCFechaVencimiento;
                funcionario.TallaCamisa = request.TallaCamisa != null ? request.TallaCamisa.ToUpper() : null;
                funcionario.TallaPantalon = request.TallaPantalon;
                funcionario.NumeroCalzado = request.NumeroCalzado;
                funcionario.UsaLentes = (bool)request.UsaLentes;
                funcionario.TipoSangreId = (int)request.TipoSangreId;
                funcionario.CorreoElectronicoPersonal = request.CorreoElectronicoPersonal;
                funcionario.CorreoElectronicoCorporativo = request.CorreoElectronicoCorporativo;
                if (request.Adjunto != null) funcionario.Adjunto = request.Adjunto;
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
