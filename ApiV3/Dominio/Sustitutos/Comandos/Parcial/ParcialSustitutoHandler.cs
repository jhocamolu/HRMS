using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Sustitutos.Comandos.Parcial
{
    public class ParcialSustitutoHandler : IRequestHandler<ParcialSustitutoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ParcialSustitutoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialSustitutoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Sustituto sustituto = contexto.Sustitutos.Find(request.Id);
                if (request.CargoASustituirId != null)
                {
                    sustituto.CargoASustituirId = (int)request.CargoASustituirId;
                }
                if (request.CargoSustitutoId != null)
                {
                    sustituto.CargoSustitutoId = (int)request.CargoSustitutoId;
                }
                if (request.CentroOperativoASutituirId != null)
                {
                    sustituto.CentroOperativoASutituirId = (int)request.CentroOperativoASutituirId;
                }
                if (request.CentroOperativoSustitutoId != null)
                {
                    sustituto.CentroOperativoASutituirId = (int)request.CentroOperativoSustitutoId;
                }
                if (request.FechaInicio != null)
                {
                    sustituto.FechaInicio = (DateTime)request.FechaInicio;
                }
                if (request.FechaFinal != null)
                {
                    sustituto.FechaFinal = (DateTime)request.FechaFinal;
                }
                if (request.Observacion != null)
                {
                    sustituto.Observacion = request.Observacion;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == EstadoRegistro.Activo)
                    {
                        sustituto.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == EstadoRegistro.Inactivo)
                    {
                        sustituto.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }


                contexto.Sustitutos.Update(sustituto);
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
