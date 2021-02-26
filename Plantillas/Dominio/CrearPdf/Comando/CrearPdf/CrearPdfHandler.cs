using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using Plantillas.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Plantillas.Dominio.CrearPdf.Comando.CrearPdf
{
    public class CrearPdfHandler : IRequestHandler<CrearPdfRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;
        private IConfiguration configuration;
        private IPeticionService peticion;
        private readonly string host;
        private int grupoDocumentoId;
        private string grupoDocumentoSlug;
        private DateTime vigencia;
        private readonly IHttpContextAccessor _httpContextAccessor;

        Dictionary<int, JObject> dictionary;

        public CrearPdfHandler(PlantillasDbContext contexto, IConfiguration configuration, IPeticionService peticion, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.peticion = peticion;
            this.dictionary = new Dictionary<int, JObject>();
            this.host = this.configuration.GetValue<string>(Constants.ServiceApi.API);
            this.vigencia = DateTime.Now;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Se exporta el documento XML que llena el contenido del pdf en un formato JSON
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommandResult> Handle(CrearPdfRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var documento = contexto.Documentos.FirstOrDefault(x => x.Slug == request.DocumentoSlug);
                var grupoDocumento = contexto.GrupoDocumentos.FirstOrDefault(g => g.Id == documento.GrupoDocumentoId);
                if (grupoDocumento == null) return CommandResult.Fail("No existe el grupo de documentos.", 404);

                this.grupoDocumentoSlug = grupoDocumento.Slug;
                this.grupoDocumentoId = grupoDocumento.Id;

                string servicio = grupoDocumento.Servicio.Replace("{id}", request.Id.ToString());

                JObject data = await this.ObtenerServicios($"{host}{servicio}");
                if (data == null) return CommandResult.Fail($"Error al conectar con el servicio. Servicio: {host}{servicio}", 500);

                string encabezado = "", cuerpo = "", pie = "", encabezadoAlto = "", pieAlto = "";

                var plantilla = await contexto.Plantillas.LastAsync(p => p.DocumentoId == documento.Id);
                if (data.ContainsKey("fechaCreacion"))
                {
                    DateTime fecha = (DateTime)data["fechaCreacion"];
                    plantilla = contexto.Plantillas.LastOrDefault(p => p.GrupoDocumentoId == grupoDocumento.Id && p.DocumentoId == documento.Id && p.FechaVigencia.Date >= fecha.Date);
                    if (plantilla == null)
                    {
                        plantilla = contexto.Plantillas.FirstOrDefault(p => p.GrupoDocumentoId == grupoDocumento.Id && p.DocumentoId == documento.Id && p.FechaVigencia.Date <= fecha.Date);
                    }

                }

                if (grupoDocumentoSlug != "certificado-laboral")
                {
                    this.vigencia = plantilla.FechaVigencia;
                }

                if (plantilla.EncabezadoId != null)
                {
                    var cabecera = contexto.ComplementoPlantillas.FirstOrDefault(c => c.Id == plantilla.EncabezadoId);
                    encabezado = await this.CombinarTag(cabecera.CuerpoDocumento.ToString(), data);
                    encabezadoAlto = cabecera.Alto.ToString();
                }

                if (plantilla.PiePaginaId != null)
                {
                    var piepagina = contexto.ComplementoPlantillas.FirstOrDefault(c => c.Id == plantilla.PiePaginaId);
                    pie = await this.CombinarTag(piepagina.CuerpoDocumento.ToString(), data);
                    pieAlto = piepagina.Alto.ToString();
                }

                if (plantilla.CuerpoDocumento != null)
                {
                    cuerpo = await this.CombinarTag(plantilla.CuerpoDocumento.ToString(), data);
                }

                return CommandResult.Success(new
                {
                    encabezado,
                    cuerpo,
                    pie,
                    encabezadoAlto,
                    pieAlto
                });

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la informacion segun los slug de las etiquetas y sus grupos de documentos validando 
        /// si la informacion esta contenida en un servicio fijo
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        private async Task<string> ObtenerDato(string slug, JObject api)
        {
            try
            {
                var grupoDocumentoEtiquetas = (from gt in contexto.GrupoDocumentoEtiquetas
                                               join e in contexto.Etiquetas on gt.EtiquetaId equals e.Id
                                               where e.Slug == slug
                                               && gt.GrupoDocumentoId == this.grupoDocumentoId
                                               select gt)
                                               .Include(gt => gt.ServicioFijo)
                                               .FirstOrDefault();
                if (grupoDocumentoEtiquetas != null)
                {
                    string replace = "";
                    string[] parts = grupoDocumentoEtiquetas.Campo.Split(".");
                    var servicioFijo = grupoDocumentoEtiquetas.ServicioFijo;
                    if (servicioFijo != null)
                    {
                        if (!this.dictionary.ContainsKey(servicioFijo.Id))
                        {
                            string services = null;
                            if (slug.StartsWith("Firma-"))
                            {
                                services = servicioFijo.Servicio.Replace("{slug}", this.grupoDocumentoSlug).Replace("{fecha}", this.vigencia.ToString("yyyy-MM-dd"));


                                this.dictionary.Add(servicioFijo.Id, await this.ObtenerServicios($"{host}{services}"));
                            }
                            else
                            {
                                this.dictionary.Add(servicioFijo.Id, await this.ObtenerServicios($"{host}{servicioFijo.Servicio}"));
                            }
                        }
                        replace = this.ObtenerValor(this.dictionary[servicioFijo.Id], parts);
                    }
                    else
                    {
                        replace = this.ObtenerValor(api, parts);
                    }
                    return replace;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }
        /// <summary>
        /// combina los tag segun la informacion o las condicienes que se realiza para la generacion del xml 
        /// con la etiqueta alcanos
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        private async Task<string> CombinarTag(string texto, JObject api)
        {
            try
            {
                texto = texto.Replace("\r\n", "");
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(texto);
                XmlNodeList nodeList = xml.GetElementsByTagName("alcanos");
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList.Item(i);
                    if (node.NodeType.Equals(XmlNodeType.Element))
                    {
                        XmlElement element = (XmlElement)node;
                        string value = element.InnerText;
                        if (value != null)
                        {
                            value = value.Replace("{", "");
                            value = value.Replace("}", "");
                            string dato = "";
                            if (value.Contains("fechaActual"))
                            {
                                if (value.Contains("dia"))
                                {
                                    dato = DateTime.Now.Day.ToString();
                                }
                                else if (value.Contains("mes"))
                                {
                                    dato = DateTime.Now.ToString();
                                }
                                else if (value.Contains("año"))
                                {
                                    dato = DateTime.Now.Year.ToString();
                                }
                                else
                                {
                                    dato = DateTime.Now.ToString("yyyy-MM-DD");
                                }
                            }
                            else
                            {
                                dato = await this.ObtenerDato(value, api);
                            }

                            dato = this.ValidacionesContenido(dato, value);

                            element.RemoveAll();
                            element.AppendChild(xml.CreateTextNode(dato != null ? dato : ""));
                        }
                    }
                }
                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    xml.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //return texto;
        }

        private string ValidacionesContenido(string dato, string value)
        {
            if (value.EndsWith("-numeroletras"))
            {
                dato = NumeroLetras.Enletras(dato);
            }
            if (value.EndsWith("-moneda"))
            {
                double money = Convert.ToDouble(dato);
                dato = money.ToString("C2");
            }
            if (value.EndsWith("-conformato"))
            {
                long numero = Convert.ToInt64(dato);
                dato = numero.ToString("N0");
            }
            if (value.EndsWith("-fechaletras"))
            {
                dato = FechasLetras.ConvertirToda(dato);
            }
            if (value.EndsWith("-mesletras"))
            {
                dato = FechasLetras.ConvertirMes(dato);
            }
            return dato;
        }

        /// <summary>
        /// obtiene el valor de los elementos json
        /// </summary>
        /// <param name="json"></param>
        /// <param name="searchs"></param>
        /// <returns></returns>
        private string ObtenerValor(JObject json, string[] searchs)
        {
            try
            {
                JObject jObject = json;
                for (int i = 0; i < searchs.Length; i++)
                {
                    string key = searchs[i];
                    if (jObject[key] != null)
                    {
                        if (i < searchs.Length - 1)
                        {
                            jObject = (JObject)jObject[key];
                        }
                        else
                        {
                            return jObject[key].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }
        /// <summary>
        /// obtiene la consulta de los servicios fijo o los servicios odata del grupo documento
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<JObject> ObtenerServicios(string url)
        {

            try
            {
                HttpResponseMessage httpResponse = await this.peticion.Get(url);
                if ((int)httpResponse.StatusCode == 200)
                {
                    return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
