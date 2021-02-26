using ApiV3.Infraestructura.Enumerador;
using ApiV3.Models;
using MediatR;
using Reclutamiento.Infraestructura.DbContexto;

using Reclutamiento.Infraestructura.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.Dashboard.Comandos.DashboarPortal
{
    public class DashboardPortalHandler : IRequestHandler<DashboardPortalRequest, CommandResult>
    {
        private readonly ReclutamientoDbContext contexto;
        

        public DashboardPortalHandler(ReclutamientoDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(DashboardPortalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dictionary<string, object> objetos = new Dictionary<string, object>();

                HojaDeVida hojaDeVida = this.contexto.HojaDeVidas.FirstOrDefault(x => x.NumeroDocumento == request.NumeroDocumento);
                if (hojaDeVida == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }

                // Consulta aplicacion del cantidado
                var aplicaciones = contexto.Candidatos.Where(x => x.HojaDeVidaId == hojaDeVida.Id &&
                                                               x.Estado != EstadoCandidato.Descartado &&
                                                               x.Estado != EstadoCandidato.NoApto &&
                                                               x.Estado != EstadoCandidato.Reprobado &&
                                                               x.EstadoRegistro == EstadoRegistro.Activo)
                                                    .Count();

                // Consulta Convocatorias abiertas
                var convovatoriasAbiertas = contexto.RequisicionPersonales.Where(x => x.Estado == EstadoRequisicionPersonal.Autorizada &&
                                                                                      x.EstadoRegistro == EstadoRegistro.Activo)
                                                                            .Count();


                int camposLlenos = 0;
                
                foreach (var prop in hojaDeVida.GetType().GetProperties())
                {
                   
                    if (prop.GetValue(hojaDeVida, null) != null &&
                        prop.Name != "EstadoRegistro" &&
                        prop.Name != "CreadoPor" &&
                        prop.Name != "FechaCreacion" &&
                        prop.Name != "ModificadoPor" &&
                        prop.Name != "FechaModificacion" &&
                        prop.Name != "EliminadoPor" &&
                        prop.Name != "FechaEliminacion"
                        )
                    {
                        camposLlenos++;
                    }
                    
                }

                int campos = hojaDeVida.GetType().GetProperties().Count() - 21;
              
                int avanceHojaDeVida = camposLlenos * 100;
                avanceHojaDeVida = avanceHojaDeVida / campos;
                objetos.Add("ConvocatoriasAbiertas", convovatoriasAbiertas);
                objetos.Add("Aplicaciones", aplicaciones);
                objetos.Add("AvanceHojaDeVida", avanceHojaDeVida);

                return CommandResult.Success(objetos);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

    }
}
