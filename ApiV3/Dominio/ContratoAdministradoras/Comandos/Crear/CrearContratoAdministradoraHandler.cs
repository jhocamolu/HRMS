using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoAdministradoras.Comandos.Crear
{
    public class CrearContratoAdministradoraHandler : IRequestHandler<CrearContratoAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearContratoAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearContratoAdministradoraRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var conAdminAnctual = (from conAdministradora in contexto.ContratoAdministradoras
                                       join administradora in contexto.Administradoras on conAdministradora.AdministradoraId equals administradora.Id
                                       where administradora.TipoAdministradoraId == request.TipoAdministradoraId
                                       && conAdministradora.ContratoId == request.ContratoId
                                       && conAdministradora.EstadoRegistro == EstadoRegistro.Activo
                                       && conAdministradora.FechaFin == null
                                       select new { id = conAdministradora.Id }).FirstOrDefault();

                if (conAdminAnctual != null)
                {

                    var hoy = DateTime.Today;
                    var ultimoDiaMesAnterior = hoy.AddDays(-hoy.Day);
                    var terminarContratoAdministradora = contexto.ContratoAdministradoras.Find(conAdminAnctual.id);
                    terminarContratoAdministradora.FechaFin = ultimoDiaMesAnterior;

                    contexto.ContratoAdministradoras.Update(terminarContratoAdministradora);
                    await contexto.SaveChangesAsync();
                }



                ContratoAdministradora contratoAdministradora = new ContratoAdministradora
                {
                    ContratoId = (int)request.ContratoId,
                    FechaInicio = (DateTime)request.FechaInicio,
                    AdministradoraId = (int)request.AdministradoraId,
                    Observacion = request.Observacion
                };



                contexto.ContratoAdministradoras.Add(contratoAdministradora);
                await contexto.SaveChangesAsync();

                if (contratoAdministradora.Contrato.ContratoAdministradoras != null)
                {
                    contratoAdministradora.Contrato.ContratoAdministradoras = null;
                }


                return CommandResult.Success(contratoAdministradora);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}