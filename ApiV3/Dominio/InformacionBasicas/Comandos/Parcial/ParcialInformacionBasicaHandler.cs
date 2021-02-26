using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.InformacionBasicas.Comandos.Parcial
{
    public class ParcialInformacionBasicaHandler : IRequestHandler<ParcialInformacionBasicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialInformacionBasicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(ParcialInformacionBasicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                InformacionBasica informacionBasica = this.contexto.InformacionBasicas.Find(request.Id);

                #region CargaDatos
                #region BASICOS
                if (request.Nombre != null) informacionBasica.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.Nit != null) informacionBasica.Nit = request.Nit;
                if (request.DigitoVerificacion != null) informacionBasica.DigitoVerificacion = (int)request.DigitoVerificacion;
                if (request.RazonSocial != null) informacionBasica.RazonSocial = request.RazonSocial;
                if (request.ActividadEconomicaId != null) informacionBasica.ActividadEconomicaId = (int)request.ActividadEconomicaId;
                #endregion

                #region LOCALIZACION
                if (request.DivisionPoliticaNivel2Id != null) informacionBasica.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                if (request.Direccion != null) informacionBasica.Direccion = request.Direccion;
                if (request.Telefono != null) informacionBasica.Telefono = request.Telefono;
                if (request.Fax != null) informacionBasica.Fax = request.Fax;
                if (request.CorreoElectronico != null) informacionBasica.CorreoElectronico = request.CorreoElectronico;
                if (request.Web != null) informacionBasica.Web = request.Web;
                #endregion

                #region EMPRESA
                if (request.FechaConstitucion != null) informacionBasica.FechaConstitucion = (DateTime)request.FechaConstitucion;
                if (request.TipoContribuyenteId != null) informacionBasica.TipoContribuyenteId = (int)request.TipoContribuyenteId;
                if (request.OperadorPagoId != null) informacionBasica.OperadorPagoId = (int)request.OperadorPagoId;
                if (request.ARLId != null) informacionBasica.ArlId = (int)request.ARLId;
                if (request.TipoDocumentoId != null) informacionBasica.TipoDocumentoId = (int)request.TipoDocumentoId;
                if (request.NaturalezaJuridicaId != null) informacionBasica.NaturalezaJuridicaId = (int)request.NaturalezaJuridicaId;
                if (request.TipoPersonaId != null) informacionBasica.TipoPersonaId = (int)request.TipoPersonaId;
                if (request.ClaseAportanteTipoAportanteId != null) informacionBasica.ClaseAportanteTipoAportanteId = (int)request.ClaseAportanteTipoAportanteId;
                if (request.CargoId != null) informacionBasica.CargoId = (int)request.CargoId;
                if (request.BeneficiarioLey1429De2010 != null) informacionBasica.BeneficiarioLey1429De2010 = (bool)request.BeneficiarioLey1429De2010;
                if (request.BeneficiarioImpuestoEquidad != null) informacionBasica.BeneficiarioImpuestoEquidad = (bool)request.BeneficiarioImpuestoEquidad;
                #endregion

                #region Estado_Registro
                if (request.Activo != null)
                {
                    informacionBasica.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) informacionBasica.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                #endregion
                #endregion

                this.contexto.InformacionBasicas.Update(informacionBasica);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(informacionBasica);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
