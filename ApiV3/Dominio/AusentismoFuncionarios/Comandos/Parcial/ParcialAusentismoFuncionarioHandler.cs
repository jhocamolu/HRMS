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

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Parcial
{
    public class ParcialAusentismoFuncionarioHandler : IRequestHandler<ParcialAusentismoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public ParcialAusentismoFuncionarioHandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ParcialAusentismoFuncionarioRequest request, CancellationToken cancellationToken)
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
                    DateTime fechaVacia = DateTime.MinValue;
                    if (actualizaTodo == true)
                    {
                        if (request.FuncionarioId != null)
                        {
                            ausentismoFuncionario.FuncionarioId = (int)request.FuncionarioId;
                        }
                        if (request.TipoAusentismoId != null)
                        {
                            ausentismoFuncionario.TipoAusentismoId = (int)request.TipoAusentismoId;
                        }
                        if (request.DiagnosticoId != null)
                        {
                            ausentismoFuncionario.DiagnosticoCieId = request.DiagnosticoId;
                        }
                        
                        if (request.FechaInicio != fechaVacia)
                        {
                            ausentismoFuncionario.FechaInicio = request.FechaInicio;
                        }
                        
                        if (request.HoraInicio != null)
                        {
                            ausentismoFuncionario.HoraInicio = (TimeSpan)request.HoraInicio;
                        }
                        if (request.HoraFin != null)
                        {
                            ausentismoFuncionario.HoraFin = (TimeSpan)request.HoraFin;
                        }
                        if (request.Activo != null)
                        {
                            if (request.Activo == true)
                            {
                                ausentismoFuncionario.EstadoRegistro = EstadoRegistro.Activo;
                            }
                            else
                            {
                                ausentismoFuncionario.EstadoRegistro = EstadoRegistro.Inactivo;
                            }
                        }

                        if (request.Aprobado != null)
                        {
                            if (request.Aprobado == true)
                            {
                                ausentismoFuncionario.Estado = EstadoAusentismo.Aprobado;
                            }
                            else
                            {
                                ausentismoFuncionario.Estado = EstadoAusentismo.Anulado;
                                ausentismoFuncionario.Justificacion = request.Justificacion;
                            }
                        }
                        if (request.NumeroIncapacidad != null)
                        {
                            ausentismoFuncionario.NumeroIncapacidad = request.NumeroIncapacidad;
                        }
                        if (request.Adjunto != null)
                        {
                            ausentismoFuncionario.Adjunto = request.Adjunto;
                        }
                        if (request.FechaIniciaReal != null)
                        {
                            ausentismoFuncionario.FechaIniciaReal = (DateTime)request.FechaIniciaReal;
                        }
                        if (request.FechaFinalReal != null)
                        {
                            ausentismoFuncionario.FechaFinalReal = (DateTime)request.FechaFinalReal;
                        }

                        ausentismoFuncionario.Observacion = request.Observacion;
                    }
                    if (actualizaFecha == true)
                    {
                        if (request.FechaFin != fechaVacia)
                        {
                            ausentismoFuncionario.FechaFin = request.FechaFin;
                        }
                    }
                        contexto.AusentismoFuncionarios.Update(ausentismoFuncionario);
                    await contexto.SaveChangesAsync();


                    if (request.ProrrogaId != null)
                    {
                        //Elimina el registro anteriormente creado
                        var prorrogaAusentismos = this.contexto.ProrrogaAusentismos.Where(x => x.AusentismoId == request.Id).ToList();

                        foreach (var borrarProrroga in prorrogaAusentismos)
                        {
                            this.contexto.ProrrogaAusentismos.Remove(borrarProrroga);
                            await contexto.SaveChangesAsync();
                        }

                        //Crea nuevo registro para prorroga ausentismo

                        ProrrogaAusentismo prorrogaAusentismoNew = new ProrrogaAusentismo();
                        prorrogaAusentismoNew.AusentismoId = (int)request.Id;
                        prorrogaAusentismoNew.ProrrogaId = (int)request.ProrrogaId;
                        contexto.ProrrogaAusentismos.Update(prorrogaAusentismoNew);
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
