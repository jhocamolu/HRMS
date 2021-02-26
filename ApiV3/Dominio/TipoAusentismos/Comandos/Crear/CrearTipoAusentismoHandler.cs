using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Crear
{
    public class CrearTipoAusentismoHandler : IRequestHandler<CrearTipoAusentismoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoAusentismoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoAusentismoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismo tipoAusentismo = new TipoAusentismo
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    ClaseAusentismoId = (int)request.ClaseAusentismoId,
                    UnidadTiempo = (UnidadTiempo)request.UnidadTiempo
                };

                string codigo = this.Extraer(request.Nombre);
                tipoAusentismo.Codigo = codigo.ToUpper();
                this.contexto.TipoAusentismos.Add(tipoAusentismo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoAusentismo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

        private string Extraer(string nombre, int? index = 0)
        {
            string[] codigoSplit = nombre.Split(" ");
            string codigo = "";
            if (codigoSplit.Length < 5)
            {
                for (int i = 0; i < codigoSplit.Length; i++)
                {
                    codigo += codigoSplit[i].Substring((int)index, 1);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    codigo += codigoSplit[i].Substring(0, 1);
                }
            }
            var validaCodigo = contexto.TipoAusentismos.FirstOrDefault(x => x.Codigo == codigo.ToUpper());
            if (validaCodigo != null)
            {
                index++;
                codigo = this.Extraer(nombre, index);
            }
            return codigo.ToUpper();
        }
    }
}
