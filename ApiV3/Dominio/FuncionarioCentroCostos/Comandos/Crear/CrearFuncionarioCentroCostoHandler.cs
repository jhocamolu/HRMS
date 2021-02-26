using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Crear
{
    public class CrearFuncionarioCentroCostoHandler : IRequestHandler<CrearFuncionarioCentroCostoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public CrearFuncionarioCentroCostoHandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(CrearFuncionarioCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                // Consulta funcionario cuyo cargo esté parametrizado como costo sicom y tenga registros en la tabla
                // Actividad funcionario con estado pendiente.
                var funcionarioCargoCostosSicom = (from f in contexto.FuncionarioDatoActuales
                                                   join c in contexto.Cargos on f.CargoId equals c.Id
                                                   join a in contexto.ActividadFuncionarios on f.Id equals a.FuncionarioId
                                                   where c.CostoSicom == true
                                                   where c.EstadoRegistro == EstadoRegistro.Activo
                                                   where f.Estado != EstadoFuncionario.Retirado
                                                   where f.Estado != EstadoFuncionario.Seleccionado
                                                   where a.Estado == EstadoActividadFuncionario.Pendiente
                                                   where a.EstadoRegistro == EstadoRegistro.Activo
                                                   select new
                                                   {
                                                       a.FuncionarioId
                                                   })
                                                   .Distinct()
                                                   .ToList();

                if (funcionarioCargoCostosSicom.Count >= 1)
                {
                    //String listaFuncionarios = "";
                    String listaFuncionarios = string.Join(",", funcionarioCargoCostosSicom);
                    listaFuncionarios = listaFuncionarios.Replace("{ FuncionarioId = ", "");
                    listaFuncionarios = listaFuncionarios.Replace("}", "");

                    // Verifica si cuenta con registros en estado pendiente
                    var verificaBorrado = repositorio.Query($@"SELECT a.Id AS ActividadFuncionarioId,b.Id AS FuncionarioCentroCostoId FROM dbo.ActividadFuncionario a
                                INNER JOIN dbo.FuncionarioCentroCosto b ON a.FuncionarioId = b.FuncionarioId
                                WHERE b.FuncionarioId IN(SELECT a.FuncionarioId FROM dbo.ActividadFuncionario a
                                INNER JOIN dbo.FuncionarioCentroCosto b ON a.FuncionarioId = b.FuncionarioId
                                WHERE a.EstadoRegistro = '{EstadoRegistro.Activo}'
                                AND b.EstadoRegistro = '{EstadoRegistro.Activo}' AND b.Estado = '{EstadoFuncionarioCentroCosto.Pendiente}'
                                AND a.Estado = '{EstadoActividadFuncionario.Pendiente}'); ")
                        .ToList();

                    verificaBorrado.Distinct();

                    List<int> controlaBorrado = new List<int>();
                    List<ActividadFuncionario> listadoActualizaActividadFuncionario = new List<ActividadFuncionario>();

                    if (verificaBorrado.Count != 0)
                    {
                        foreach (var itemBorrado in verificaBorrado)
                        {
                            bool confirmaBorrado = controlaBorrado.Contains(itemBorrado.FuncionarioCentroCostoId);
                            if (!confirmaBorrado)
                            {
                                //borradoActividadFuncionarioCentroCosto
                                this.contexto.Database
                                             .ExecuteSqlRaw($"DELETE FROM {typeof(ActividadFuncionarioCentroCosto).Name} WHERE FuncionarioCentroCostoId ={itemBorrado.FuncionarioCentroCostoId}");

                                //borradoFuncionarioCentroCosto
                                this.contexto.Database
                                         .ExecuteSqlRaw($"DELETE FROM {typeof(FuncionarioCentroCosto).Name} WHERE Id ={itemBorrado.FuncionarioCentroCostoId}");

                                controlaBorrado.Add(itemBorrado.FuncionarioCentroCostoId);
                            }

                            var actualizaEstadoActividadFuncionario = contexto.ActividadFuncionarios.Find(itemBorrado.ActividadFuncionarioId);
                            actualizaEstadoActividadFuncionario.Estado = EstadoActividadFuncionario.Pendiente;

                            listadoActualizaActividadFuncionario.Add(actualizaEstadoActividadFuncionario);
                        }
                        this.contexto.ActividadFuncionarios.UpdateRange(listadoActualizaActividadFuncionario);
                        await this.contexto.SaveChangesAsync();
                    }

                    //Agrupa por centro de costo y funcionario la cantidad
                    var consulta = repositorio.Query($@" SELECT 
                                    b.FuncionarioId,
                                    a.Id as ActividadCentroCostoId,
                                    sum(b.Cantidad) as Suma,
                                    max(b.FechaFinalizacion) as FechaCorte
                                    FROM dbo.ActividadCentroCosto a
                                    INNER JOIN dbo.ActividadFuncionario b ON a.ActividadId = b.ActividadId 
                                                                            AND a.MunicipioId = b.MunicipioId  
                                                                            AND b.Estado = '{EstadoActividadFuncionario.Pendiente}'
                                                                           AND b.EstadoRegistro = '{EstadoRegistro.Activo}'
                                    WHERE b.FuncionarioId IN({ listaFuncionarios })
                                    GROUP BY b.FuncionarioId, a.Id
                                    ORDER BY b.FuncionarioId, a.Id;")
                                    .ToList();

                    if (consulta != null)
                    {
                        // Listado de funcionariId ingresados en tabla FuncionarioCentroCosto
                        List<int> funcionariosEnCentroDeCosto = new List<int>();
                        foreach (var item in consulta)
                        {
                            var funcionarioCentroCosto = new FuncionarioCentroCosto();
                            funcionarioCentroCosto.FuncionarioId = item.FuncionarioId;
                            funcionarioCentroCosto.ActividadCentroCostoId = item.ActividadCentroCostoId;
                            funcionarioCentroCosto.Cantidad = item.Suma;
                            funcionarioCentroCosto.Ponderado = 0;
                            funcionarioCentroCosto.Porcentaje = 0;
                            funcionarioCentroCosto.FechaCorte = item.FechaCorte;
                            funcionarioCentroCosto.FormaRegistro = FormaRegistroFuncionarioCentroCosto.Automatico;
                            funcionarioCentroCosto.Estado = EstadoFuncionarioCentroCosto.Pendiente;
                            this.contexto.FuncionarioCentroCostos.Add(funcionarioCentroCosto);
                            await this.contexto.SaveChangesAsync();

                            // Se construye una lista con el distinct de funcionarioId de los registros procesados.
                            bool existeEnLista = funcionariosEnCentroDeCosto.Contains(item.FuncionarioId);
                            if (!existeEnLista)
                            {
                                funcionariosEnCentroDeCosto.Add(item.FuncionarioId);
                            }

                            //Consulta la actividad centro de costo
                            var actividadCentroCosto = contexto.ActividadCentroCostos.Find(funcionarioCentroCosto.ActividadCentroCostoId);

                            if (actividadCentroCosto != null)
                            {
                                var actividadFuncionario = contexto.ActividadFuncionarios.Where(x => x.FuncionarioId == funcionarioCentroCosto.FuncionarioId &&
                                                                                                          x.ActividadId == actividadCentroCosto.ActividadId &&
                                                                                                          x.MunicipioId == actividadCentroCosto.MunicipioId &&
                                                                                                          x.Estado == EstadoActividadFuncionario.Pendiente
                                                                                                    )
                                                                                        .ToList();
                                if (actividadFuncionario.Count != 0)
                                {
                                    //Se crea relación con actividad centro costo
                                    foreach (var itemActividadFuncionario in actividadFuncionario)
                                    {
                                        var actividadFuncionarioCentroCosto = new ActividadFuncionarioCentroCosto();
                                        actividadFuncionarioCentroCosto.ActividadFuncionarioId = itemActividadFuncionario.Id;
                                        actividadFuncionarioCentroCosto.FuncionarioCentroCostoId = funcionarioCentroCosto.Id;
                                        this.contexto.ActividadFuncionarioCentroCostos.Add(actividadFuncionarioCentroCosto);
                                        await this.contexto.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                        // actualiza el estado de actividad funcionario
                        List<ActividadFuncionario> actividadFuncionarios = contexto.ActividadFuncionarios.Where(x => x.Estado == EstadoActividadFuncionario.Pendiente &&
                                                                                                                    x.EstadoRegistro == EstadoRegistro.Activo)
                                                                                                         .ToList();
                        foreach (var actualizaActividadFuncionarios in actividadFuncionarios)
                        {
                            actualizaActividadFuncionarios.Estado = EstadoActividadFuncionario.Aplicado;
                        }
                        this.contexto.ActividadFuncionarios.UpdateRange(actividadFuncionarios);
                        await this.contexto.SaveChangesAsync();

                        //Recorre la lista de funcionarios a los cuales se le ingresaron registros en la tabla FuncionarioCentroCosto
                        foreach (var ingresado in funcionariosEnCentroDeCosto)
                        {
                            var datoFuncionarioCentroCostos = contexto.FuncionarioCentroCostos.Include(x => x.ActividadCentroCosto.Actividad)
                                                                                              .Where(x => x.FuncionarioId == ingresado &&
                                                                                                          x.Porcentaje == 0 &&
                                                                                                          x.Ponderado == 0 &&
                                                                                                          x.FechaCreacion.Value.Date == DateTime.Now.Date)
                                                                                             .ToList();
                            if (datoFuncionarioCentroCostos.Count != 0)
                            {
                                decimal acumulaValorPonderado = 0;
                                decimal valorPonderado = 0;
                                decimal valorComplejidad = 0;

                                foreach (var datoFuncionarioCentroCostoPonderado in datoFuncionarioCentroCostos)
                                {
                                    valorComplejidad = datoFuncionarioCentroCostoPonderado.ActividadCentroCosto.Actividad.ValorComplejidad;
                                    valorPonderado = ((int)datoFuncionarioCentroCostoPonderado.Cantidad) + (((int)datoFuncionarioCentroCostoPonderado.Cantidad) * valorComplejidad);

                                    // actualiza valor ponderado en la tabla
                                    var actualizaValorPonderado = contexto.FuncionarioCentroCostos.Find(datoFuncionarioCentroCostoPonderado.Id);
                                    actualizaValorPonderado.Ponderado = (Double)valorPonderado;
                                    this.contexto.FuncionarioCentroCostos.Update(actualizaValorPonderado);
                                    await this.contexto.SaveChangesAsync();

                                    acumulaValorPonderado += valorPonderado;
                                }

                                if (acumulaValorPonderado != 0)
                                {
                                    double valorPorcentaje = 0;
                                    foreach (var datoFuncionarioCentroCostoPorcentaje in datoFuncionarioCentroCostos)
                                    {
                                        valorPorcentaje = (Double)datoFuncionarioCentroCostoPorcentaje.Ponderado / (Double)acumulaValorPonderado;

                                        // actualiza valor ponderado en la tabla
                                        var actualizaValorPorcentaje = contexto.FuncionarioCentroCostos.Find(datoFuncionarioCentroCostoPorcentaje.Id);
                                        actualizaValorPorcentaje.Porcentaje = (Double)valorPorcentaje;
                                        this.contexto.FuncionarioCentroCostos.Update(actualizaValorPorcentaje);
                                        await this.contexto.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                }

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
        /*private async Task<bool> valida()
        {
            
        }*/
    }
}
