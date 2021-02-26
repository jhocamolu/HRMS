using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiV3.Infraestructura.Filtros
{
    public static class Interceptacion
    {
        public static List<int> FiltroVacaciones(Funcionario funcionario, NominaDbContext contexto)
        {
            try
            {
                List<int> sol = contexto.SolicitudVacaciones.FromSqlInterpolated($@"
                    SELECT [Id]
                          ,[FuncionarioId]
                          ,[LibroVacacionesId]
                          ,[FechaInicioDisfrute]
                          ,[DiasDisfrute]
                          ,[FechaFinDisfrute]
                          ,[DiasDinero]
                          ,[Observacion]
                          ,[Estado]
                          ,[Justificacion]
                          ,[FechaPago]
                          ,[Remuneracion]
                          ,[FechaRegreso]
                          ,[EstadoRegistro]
                          ,[CreadoPor]
                          ,[FechaCreacion]
                          ,[ModificadoPor]
                          ,[FechaModificacion]
                          ,[EliminadoPor]
                          ,[FechaEliminacion]
                          ,[NominaFuncionarioId]
                      FROM[dbo].[SolicitudVacaciones]
                    WHERE [FuncionarioId] = {funcionario.Id}
            ").Select(x => x.Id).ToList();
                return sol;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
