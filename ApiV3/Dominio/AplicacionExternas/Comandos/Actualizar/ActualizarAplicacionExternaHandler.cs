using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Actualizar
{
    public class ActualizarAplicacionExternaHandler : IRequestHandler<ActualizarAplicacionExternaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarAplicacionExternaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarAplicacionExternaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExterna aplicacionExterna = this.contexto.AplicacionExternas.Find(request.Id);
                aplicacionExterna.Nombre = Texto.TipoOracion(request.Nombre);
                aplicacionExterna.Descripcion = request.Descripcion;
                aplicacionExterna.Revisa = (TipoAplicacionExterna)request.Revisa;
                aplicacionExterna.Aprueba = (TipoAplicacionExterna)request.Aprueba;
                aplicacionExterna.Autoriza = (TipoAplicacionExterna)request.Autoriza;

                contexto.AplicacionExternas.Update(aplicacionExterna);
                await contexto.SaveChangesAsync();

                // Verifica
                if (request.Revisa != TipoAplicacionExterna.Otro)
                {
                    BorrarAplicacionExternaCargo(TipoAplicacionExternaCargo.Revision, aplicacionExterna.Id);
                }
                if (request.Aprueba != TipoAplicacionExterna.Otro)
                {
                    BorrarAplicacionExternaCargo(TipoAplicacionExternaCargo.Aprobacion, aplicacionExterna.Id);
                }
                if (request.Autoriza != TipoAplicacionExterna.Otro)
                {
                    BorrarAplicacionExternaCargo(TipoAplicacionExternaCargo.Autorizacion, aplicacionExterna.Id);
                }

                return CommandResult.Success(aplicacionExterna);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

        private bool BorrarAplicacionExternaCargo(TipoAplicacionExternaCargo tipo, int aplicacionExternaId)
        {
            //Consulta si tiene creado registros en aplicacion externa cargo
            var aplicacionExternaCargos = this.contexto.AplicacionExternaCargos.Where(x => x.Tipo == tipo &&
                                                                                               x.AplicacionExternaId == aplicacionExternaId &&
                                                                                               x.EstadoRegistro == EstadoRegistro.Activo)
                                                                                     .ToList();
            if (aplicacionExternaCargos.Count >= 1)
            {
                string tabla = typeof(AplicacionExternaCargo).Name;
                this.contexto.Database
                             .ExecuteSqlRaw($"DELETE FROM {tabla} WHERE AplicacionExternaId ={ aplicacionExternaId}");

            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
