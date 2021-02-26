using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Crear
{
    public class CrearNominaFuncionarioHandler : IRequestHandler<CrearNominaFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearNominaFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNominaFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<NominaFuncionario> lista = new List<NominaFuncionario>();
                foreach (var item in request.Funcionarios)
                {
                    var busqueda = await contexto.NominaFuncionarios.FirstOrDefaultAsync(x => x.NominaId == request.NominaId && x.FuncionarioId == item);
                    if (busqueda == null)
                    {
                        NominaFuncionario nominaFuncionario = new NominaFuncionario
                        {
                            NominaId = request.NominaId,
                            FuncionarioId = item,
                            NetoPagar = 0,
                            Estado = EstadoNominaFuncionario.Asignado
                        };
                        lista.Add(nominaFuncionario);
                    }
                }
                lista.Count();
                contexto.NominaFuncionarios.AddRange(lista);
                await contexto.SaveChangesAsync();

                var nomina = contexto.Nominas.FirstOrDefault(x => x.Id == request.NominaId);
                nomina.Estado = EstadoNomina.Modificada;

                contexto.Nominas.Update(nomina);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(lista);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
