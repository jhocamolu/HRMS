using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.InformacionBasicas.Comandos.Crear
{
    public class CrearInformacionBasicaHandler : IRequestHandler<CrearInformacionBasicaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public CrearInformacionBasicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(CrearInformacionBasicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                InformacionBasica informacionBasica = new InformacionBasica();
                #region CargaDatos
                informacionBasica.Nombre = Texto.TipoOracion(request.Nombre);
                informacionBasica.Nit = request.Nit;
                informacionBasica.DigitoVerificacion = (int)request.DigitoVerificacion;
                informacionBasica.RazonSocial = Texto.TipoOracion(request.RazonSocial);
                informacionBasica.ActividadEconomicaId = (int)request.ActividadEconomicaId;
                informacionBasica.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                informacionBasica.Direccion = Texto.TipoOracion(request.Direccion);
                informacionBasica.Telefono = request.Telefono;
                informacionBasica.CorreoElectronico = request.CorreoElectronico;
                informacionBasica.Web = request.Web;
                informacionBasica.FechaConstitucion = (DateTime)request.FechaConstitucion;
                informacionBasica.TipoContribuyenteId = (int)request.TipoContribuyenteId;
                informacionBasica.OperadorPagoId = (int)request.OperadorPagoId;
                informacionBasica.ArlId = (int)request.ARLId;
                informacionBasica.Fax = request.Fax;
                informacionBasica.TipoDocumentoId = (int)request.TipoDocumentoId;
                informacionBasica.NaturalezaJuridicaId = (int)request.NaturalezaJuridicaId;
                informacionBasica.TipoPersonaId = (int)request.TipoPersonaId;
                informacionBasica.ClaseAportanteTipoAportanteId = (int)request.ClaseAportanteTipoAportanteId;
                informacionBasica.CargoId = (int)request.CargoId;
                informacionBasica.BeneficiarioImpuestoEquidad = (bool)request.BeneficiarioImpuestoEquidad;
                informacionBasica.BeneficiarioLey1429De2010 = (bool)request.BeneficiarioLey1429De2010;

                #endregion



                this.contexto.InformacionBasicas.Add(informacionBasica);
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
