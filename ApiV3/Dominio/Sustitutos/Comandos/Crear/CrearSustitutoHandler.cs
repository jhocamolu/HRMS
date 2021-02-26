using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Sustitutos.Comandos.Crear
{
    public class CrearSustitutoHandler : IRequestHandler<CrearSustitutoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public CrearSustitutoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearSustitutoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Sustituto sustituto = new Sustituto();
                sustituto.CargoASustituirId = (int)request.CargoASustituirId;
                sustituto.CargoSustitutoId = (int)request.CargoSustitutoId;
                if (request.CentroOperativoASutituirId != null)
                {
                    sustituto.CentroOperativoASutituirId = (int)request.CentroOperativoASutituirId;
                }
                if (request.CentroOperativoSustitutoId != null)
                {
                    sustituto.CentroOperativoSustitutoId = (int)request.CentroOperativoSustitutoId;
                }
                sustituto.FechaInicio = (DateTime)request.FechaInicio;
                sustituto.FechaFinal = (DateTime)request.FechaFinal;
                sustituto.Observacion = request.Observacion;

                contexto.Sustitutos.Add(sustituto);
                await contexto.SaveChangesAsync();

                if (sustituto.CargoASustituir != null)
                {
                    sustituto.CargoASustituir.ASustituir = new List<Sustituto>();
                    sustituto.CargoASustituir.Sustituto = new List<Sustituto>();
                }


                if (sustituto.CargoSustituto != null)
                {
                    sustituto.CargoSustituto.ASustituir = new List<Sustituto>();
                    sustituto.CargoSustituto.Sustituto = new List<Sustituto>();
                }

                if (sustituto.CentroOperativoASutituir != null)
                {
                    sustituto.CentroOperativoASutituir.CentroASustituir = new List<Sustituto>();
                    sustituto.CentroOperativoASutituir.CentroSustituto = new List<Sustituto>();
                }

                if (sustituto.CentroOperativoSustituto != null)
                {
                    sustituto.CentroOperativoSustituto.CentroASustituir = new List<Sustituto>();
                    sustituto.CentroOperativoSustituto.CentroSustituto = new List<Sustituto>();
                }

                return CommandResult.Success(sustituto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
