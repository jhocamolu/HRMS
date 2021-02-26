using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Actualizar
{
    public class ActualizarAusentismoFuncionarioHandler : IRequestHandler<ActualizarAusentismoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public ActualizarAusentismoFuncionarioHandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ActualizarAusentismoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AusentismoFuncionario ausentismoFuncionario = this.contexto.AusentismoFuncionarios.Find(request.Id);

                if (ausentismoFuncionario.Estado == EstadoAusentismo.Registrado || ausentismoFuncionario.Estado == EstadoAusentismo.Aprobado)
                {
                    // Consulta si se encuentra  asociado a una liquidación de nómina
                    var liquidacionNomina = repositorio.Query($@"SELECT top 1 n.Estado FROM dbo.NominaDetalle nd
                                INNER JOIN dbo.NominaFuenteNovedad nfn ON nd.NominaFuenteNovedadId  = nfn.id
                                INNER JOIN dbo.NominaFuncionario nf ON nd.NominaFuncionarioId = nf.Id
                                INNER JOIN dbo.Nomina n ON nf.NominaId = n.Id
                                WHERE nfn.Modulo = '{ModuloSistema.Ausentismos}'
                                AND nfn.ModuloRegistroId = {ausentismoFuncionario.Id}
                                AND nfn.EstadoRegistro =  '{EstadoRegistro.Activo}'
                                AND nd.EstadoRegistro =  '{EstadoRegistro.Activo}'
                                AND nf.EstadoRegistro =  '{EstadoRegistro.Activo}'
                                AND n.EstadoRegistro =  '{EstadoRegistro.Activo}'
                                ORDER BY n.FechaFinal;")
                            .ToList();

                    var actualizaTodo = false;
                    var actualizaFecha = false;
                    if (liquidacionNomina.Count == 0)
                    {
                        actualizaTodo = true;
                        actualizaFecha = true;
                    }
                    else
                    {
                        actualizaFecha = true;
                    }

                    if (actualizaTodo == true)
                    {
                        ausentismoFuncionario.TipoAusentismoId = request.TipoAusentismoId;
                        ausentismoFuncionario.DiagnosticoCieId = request.DiagnosticoId;
                        ausentismoFuncionario.FechaInicio = request.FechaInicio;

                        if (request.FechaIniciaReal != null)
                        {
                            ausentismoFuncionario.FechaIniciaReal = (DateTime)request.FechaIniciaReal;
                        }
                        if (request.FechaFinalReal != null)
                        {
                            ausentismoFuncionario.FechaFinalReal = (DateTime)request.FechaFinalReal;
                        }
                        ausentismoFuncionario.HoraInicio = (TimeSpan)request.HoraInicio;
                        ausentismoFuncionario.HoraFin = (TimeSpan)request.HoraFin;
                        ausentismoFuncionario.NumeroIncapacidad = request.NumeroIncapacidad;

                        if (request.Estado != null)
                        {
                            ausentismoFuncionario.Estado = (EstadoAusentismo)request.Estado;
                        }
                        if (request.Adjunto != null)
                        {
                            ausentismoFuncionario.Adjunto = request.Adjunto;
                        }
                        ausentismoFuncionario.Observacion = request.Observacion;

                    }
                    if (actualizaFecha == true)
                    {
                        ausentismoFuncionario.FechaFin = request.FechaFin;
                    }
                    contexto.AusentismoFuncionarios.Update(ausentismoFuncionario);
                    await contexto.SaveChangesAsync();

                    //Elimina el registro anteriormente creado
                    var prorrogaAusentismos = this.contexto.ProrrogaAusentismos.Where(x => x.AusentismoId == request.Id).ToList();

                    foreach (var borrarProrroga in prorrogaAusentismos)
                    {
                        this.contexto.ProrrogaAusentismos.Remove(borrarProrroga);
                        await contexto.SaveChangesAsync();
                    }

                    if (request.ProrrogaId != null)
                    {
                        //Crea nuevo registro para prorroga ausentismo

                        ProrrogaAusentismo prorrogaAusentismoNew = new ProrrogaAusentismo();
                        prorrogaAusentismoNew.AusentismoId = request.Id;
                        prorrogaAusentismoNew.ProrrogaId = (int)request.ProrrogaId;
                        contexto.ProrrogaAusentismos.Add(prorrogaAusentismoNew);
                        await contexto.SaveChangesAsync();
                    }
                    if (ausentismoFuncionario.AusentismoDe != null)
                    {
                        ausentismoFuncionario.AusentismoDe = new List<ProrrogaAusentismo>();
                        ausentismoFuncionario.ProrrogaDe = new List<ProrrogaAusentismo>();
                    }
                }
                return CommandResult.Success(ausentismoFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
