using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar crear registros de la entidad Funcionarios
/// </summary>

namespace ApiV3.Dominio.Funcionarios.Comandos.Crear
{
    public class CrearFuncionarioHandler : IRequestHandler<CrearFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Funcionario funcionarios = new Funcionario
                {
                    #region Carga de Datos
                    PrimerNombre = Texto.LetraCapital(request.PrimerNombre),
                    SegundoNombre = request.SegundoNombre != null ? Texto.LetraCapital(request.SegundoNombre) : null,
                    PrimerApellido = Texto.LetraCapital(request.PrimerApellido),
                    SegundoApellido = request.SegundoApellido != null ? Texto.LetraCapital(request.SegundoApellido) : null,
                    SexoId = (int)request.SexoId,
                    EstadoCivilId = (int)request.EstadoCivilId,
                    OcupacionId = (int)request.OcupacionId,
                    Pensionado = (bool)request.Pensionado,
                    FechaNacimiento = (DateTime)request.FechaNacimiento,
                    DivisionPoliticaNivel2OrigenId = (int)request.DivisionPoliticaNivel2OrigenId,
                    TipoDocumentoId = (int)request.TipoDocumentoId,
                    NumeroDocumento = request.NumeroDocumento,
                    FechaExpedicionDocumento = (DateTime)request.FechaExpedicionDocumento,
                    DivisionPoliticaNivel2ExpedicionDocumentoId = (int)request.DivisionPoliticaNivel2ExpedicionDocumentoId,
                    Nit = request.Nit,
                    DigitoVerificacion = (int)request.DigitoVerificacion,
                    DivisionPoliticaNivel2ResidenciaId = (int)request.DivisionPoliticaNivel2ResidenciaId,
                    Celular = request.Celular,
                    TelefonoFijo = request.TelefonoFijo,
                    TipoViviendaId = (int)request.TipoViviendaId,
                    Direccion = request.Direccion,
                    ClaseLibretaMilitarId = request.ClaseLibretaMilitarId,
                    NumeroLibreta = request.NumeroLibreta,
                    Distrito = request.Distrito,
                    LicenciaConduccionAId = request.LicenciaConduccionAId,
                    LicenciaConduccionAFechaVencimiento = request.LicenciaConduccionAFechaVencimiento,
                    LicenciaConduccionBId = request.LicenciaConduccionBId,
                    LicenciaConduccionBFechaVencimiento = request.LicenciaConduccionBFechaVencimiento,
                    LicenciaConduccionCId = request.LicenciaConduccionCId,
                    LicenciaConduccionCFechaVencimiento = request.LicenciaConduccionCFechaVencimiento,
                    TallaCamisa = request.TallaCamisa != null ? request.TallaCamisa.ToUpper() : null,
                    TallaPantalon = request.TallaPantalon,
                    NumeroCalzado = request.NumeroCalzado,
                    UsaLentes = (bool)request.UsaLentes,
                    TipoSangreId = (int)request.TipoSangreId,
                    CorreoElectronicoPersonal = request.CorreoElectronicoPersonal,
                    CorreoElectronicoCorporativo = request.CorreoElectronicoCorporativo,
                    #endregion
                };


                funcionarios.Estado = EstadoFuncionario.Seleccionado;
                this.contexto.Funcionarios.Add(funcionarios);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(funcionarios);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
