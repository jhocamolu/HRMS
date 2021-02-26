using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApiV3.Dominio.HoraExtras.TareasProgramadas.Importar
{
    public class ApiHoraExtra
    {
        public int id { get; set; }
        public string identificacion { get; set; }
        public DateTime fechaLiquida { get; set; }
        public string tipo { get; set; }
        public double cantidad { get; set; }
        public double? valor { get; set; }
        public string actividad { get; set; }
    }

    public class ImportarHoraExtrasHandler : IRequestHandler<ImportarHoraExtrasRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IPeticionService peticionService;

        public ImportarHoraExtrasHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticionService)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticionService = peticionService;
        }

        public async Task<CommandResult> Handle(ImportarHoraExtrasRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hoy = DateTime.Now;
                var identificacionActual = "";
                int idFuncionarioActual = 0;
                var noExisteFuncionario = "";
                List<HoraExtra> listaHorasExtras = new List<HoraExtra>();

                var fechaInicial = request.FechaInicial ?? hoy.AddMonths(-2);
                var fechaFinal = request.FechaFinal ?? hoy;
                var urlfechaInicial = HttpUtility.UrlEncode(fechaInicial.ToString("MM/dd/yyyy"));
                var urlfechaFinal = HttpUtility.UrlEncode(fechaFinal.ToString("MM/dd/yyyy"));

                string api = this.configuration.GetValue<string>(Constants.HorasExtras.Api).ToString();
                var url = $"{api}/HoraExtra/GetHorasExtras?FechaLiquidacionInicio={urlfechaInicial}&FechaLiquidacionFin={urlfechaFinal}";
                var tokens = httpContextAccessor.HttpContext.Request.Headers["JwtToken"][0];

                var response = await peticionService.Get(url);
                response.Headers.Add("jwttoken", tokens.ToString());

                if (response.IsSuccessStatusCode == false)
                {
                    var responseSplit = response.ToString().Split(",")[0];
                    var estatus = responseSplit.ToString().Split(":")[1];

                    return CommandResult.Fail("Error al consumir API horas extras", int.Parse(estatus));
                }

                var content = response.Content.ReadAsStreamAsync().Result;
                var contentIEnumerable = await JsonSerializer.DeserializeAsync
                    <IEnumerable<ApiHoraExtra>>(content);

                foreach (var item in contentIEnumerable)
                {
                    var existeHoraExtra = contexto.HoraExtras.FirstOrDefault(x => x.OrigenId == item.id.ToString());
                    Funcionario funcionario = null;
                    if (existeHoraExtra == null)
                    {
                        Infraestructura.Enumerador.TipoHoraExtra tipo = (Infraestructura.Enumerador.TipoHoraExtra)Enum.Parse(typeof(Infraestructura.Enumerador.TipoHoraExtra), item.tipo, true);

                        if (identificacionActual != item.identificacion)
                        {
                            identificacionActual = item.identificacion;
                            funcionario = this.contexto.Funcionarios.FirstOrDefault(x => x.NumeroDocumento == item.identificacion && x.EstadoRegistro == EstadoRegistro.Activo);
                            if (funcionario is null)
                            {

                                noExisteFuncionario += $"El funcionario con numero de documento {item.identificacion}, no existe o no esta activo.{Environment.NewLine}";
                                Console.WriteLine("no existe....." + item.identificacion);
                                continue;
                            }
                            idFuncionarioActual = funcionario.Id;
                        }
                        if (funcionario != null)
                        {
                            var horaExtra = new HoraExtra { };
                            horaExtra.FuncionarioId = idFuncionarioActual;
                            horaExtra.TipoHoraExtraId = (int)tipo;
                            horaExtra.Fecha = item.fechaLiquida;
                            horaExtra.Cantidad = item.cantidad.ToString();
                            horaExtra.Valor = item.valor;
                            horaExtra.FormaRegistro = FormaRegistroHoraExtra.Automatico;
                            horaExtra.Estado = EstadoHoraExtra.Pendiente;
                            horaExtra.OrigenId = item.id.ToString();
                            horaExtra.Observacion = item.actividad;

                            this.contexto.HoraExtras.AddRange(horaExtra);
                        }
                    }

                }
                //Insertamos mesnaje de los funionarios que no se les pudo importar las Horas extras
                if (!string.IsNullOrEmpty(noExisteFuncionario))
                {
                    string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";
                    var mensaje = "Validar los siguientes funcionarios en GHESTIC." +
                                 $"{Environment.NewLine} {noExisteFuncionario}";
                    string tabla = typeof(Models.NoMapeado._logError).Name;
                    string Procedimiento = typeof(ImportarHoraExtrasHandler).Name;
                    int linea = 90;
                    int numero = 547; // SQL Error [547] The INSERT statement conflicted with the FOREIGN KEY constraint
                    this.contexto.Database
                                 .ExecuteSqlRaw($"INSERT INTO util.{tabla} (Procedimiento, mensaje, linea, numero, usuario )" +
                                                $"VALUES ('{Procedimiento}','{mensaje}', '{linea}', {numero}, '{usuario}')");
                }

                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
