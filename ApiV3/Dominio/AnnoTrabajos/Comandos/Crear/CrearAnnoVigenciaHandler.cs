using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AnnoTrabajos.Comandos.Crear
{
    public class CrearAnnoVigenciaHandler : IRequestHandler<CrearAnnoVigenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearAnnoVigenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearAnnoVigenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AnnoVigencia annoVigencia = new AnnoVigencia();
                annoVigencia.Anno = (int)request.Anno;
                annoVigencia.Estado = EstadoAnnoVigencia.Cerrado;
                contexto.AnnoVigencias.Add(annoVigencia);
                await contexto.SaveChangesAsync();

                // Consulta año vigente
                var annoVigente = contexto.AnnoVigencias.FirstOrDefault(x => x.Estado == EstadoAnnoVigencia.Vigente &&
                                                                            x.EstadoRegistro == EstadoRegistro.Activo);
                if (annoVigente != null)
                {

                    // Duplicar el registro de la tabla parámetro general
                    var consultaParametrosGeneral = contexto.ParametroGenerales.Where(x => x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                            x.AnnoVigenciaId == annoVigente.Id)
                                                                                .ToList();
                    if (consultaParametrosGeneral.Count >= 1)
                    {
                        foreach (var item in consultaParametrosGeneral)
                        {
                            var nuevoParametroGeneral = new ParametroGeneral();
                            nuevoParametroGeneral.Alias = item.Alias;
                            nuevoParametroGeneral.AnnoVigenciaId = annoVigencia.Id;
                            nuevoParametroGeneral.Ayuda = item.Ayuda;
                            nuevoParametroGeneral.CategoriaParametroId = item.CategoriaParametroId;
                            nuevoParametroGeneral.CreadoPor = "sistema";
                            nuevoParametroGeneral.EstadoRegistro = EstadoRegistro.Activo;
                            nuevoParametroGeneral.Etiqueta = item.Etiqueta;
                            nuevoParametroGeneral.FechaCreacion = DateTime.Now.Date;
                            nuevoParametroGeneral.HtmlOpcion = item.HtmlOpcion;
                            nuevoParametroGeneral.Item = item.Item;
                            nuevoParametroGeneral.Obligatorio = item.Obligatorio;
                            nuevoParametroGeneral.Orden = item.Orden;
                            nuevoParametroGeneral.Tipo = item.Tipo;
                            nuevoParametroGeneral.Valor = item.Valor;

                            contexto.ParametroGenerales.Add(nuevoParametroGeneral);
                            await contexto.SaveChangesAsync();
                        }
                    }
                }
                return CommandResult.Success(annoVigencia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }


    }
}
