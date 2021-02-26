using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReportingServicesConection;
using System.ServiceModel;
using Reportes.Models;
using Reportes.Support;
using Reportes.Infraestructura.Utilidades;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Infraestructura.Interface
{
    public class ReportService : IReportService
    {
        private IConfiguration configuration;
        private string EndpointUrl;
        private string UserName;
        private string Password;
        private string Domain;
        private string ReportPath;
        private string ruta = Path.Combine(Directory.GetCurrentDirectory(), "Public");
        private string ReportWidth;
        private string ReportHeight;
        private string ReportFormat;// Other options include WORDOPENXML and EXCELOPENXML
        private string ReportExtension;
        const string HistoryId = null;

        public ReportService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// Obtiene la ruta de directorio de archivo
        /// </summary>
        /// <param name="reporte">Se envia el objeto completo de reportes</param>
        /// <param name="param"></param>
        /// <param name="options"></param>
        /// <returns>ruta</returns>
        public async Task<string> Ruta(Reporte reporte, Dictionary<string, string> param = null, Dictionary<string, string> options = null)
        {
            try
            {
                this.EndpointUrl = this.configuration.GetValue<string>(Constants.ConexionReporting.Service);
                this.UserName = this.configuration.GetValue<string>(Constants.ConexionReporting.UserName);
                this.Password = this.configuration.GetValue<string>(Constants.ConexionReporting.Password);
                this.Domain = this.configuration.GetValue<string>(Constants.ConexionReporting.Domain);
                string ambiente = this.configuration.GetValue<string>(Constants.Ambiente);
                this.ReportWidth = reporte.Ancho;
                this.ReportHeight = reporte.Alto;
                this.ReportFormat = reporte.Formato.ToString();
                this.ReportExtension = reporte.Extension.ToString();
                this.ReportPath = "/" + ambiente + "/" + reporte.Path;

                string content = await this.RunReport(reporte.Nombre, param);
                var prueba = content;
                return prueba;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Eejcuta instrucciones para acceso, rederizacion y guardado de archivo
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="parametros"></param>
        /// <returns>Nombre de archivo</returns>
        public async Task<string> RunReport(string nombre, Dictionary<string, string> parametros = null)
        {
            try
            {
                ReportExecutionServiceSoapClient rs = await CreateClient();
                var trustedHeader = new TrustedUserHeader();

                LoadReportResponse loadReponse = await LoadReport(rs, trustedHeader);
                if (parametros != null)
                {
                    await AddParametersToTheReport(rs, loadReponse.ExecutionHeader, trustedHeader, parametros);
                }

                RenderResponse response = await RenderReportByteArrayAsync(loadReponse.ExecutionHeader, trustedHeader, rs, ReportFormat, ReportWidth, ReportHeight);
                nombre = nombre.Replace(" ", "");
                nombre = nombre.Replace("/", "");
                nombre = nombre.Replace("-", "");
                nombre = nombre.Replace("_", "");
                nombre = nombre.Replace("\\", "");
                nombre = nombre.Replace("(", "");
                nombre = nombre.Replace(")", "");
                nombre = Texto.QuitarAcentos(nombre);
                string name = $"{nombre}_{DateTime.Now.ToString(format: "yyyyMMddHHmmss")}.{this.ReportExtension.ToLower()}";
                SaveResultToFile(response.Result, name);
                var fs = File.OpenRead($"{ruta}\\{name}");
                return name;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Carga el reporte
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="trustedHeader"></param>
        /// <returns></returns>
        private async Task<LoadReportResponse> LoadReport(ReportExecutionServiceSoapClient rs, TrustedUserHeader trustedHeader)
        {
            try
            {
                // Get the report and set the execution header.
                // Failure to set the execution header will result in this error: "The session identifier is missing. A session identifier is required for this operation."
                // See https://social.msdn.microsoft.com/Forums/sqlserver/en-US/17199edb-5c63-4815-8f86-917f09809504/executionheadervalue-missing-from-reportexecutionservicesoapclient
                LoadReportResponse loadReponse = await rs.LoadReportAsync(trustedHeader, ReportPath, HistoryId);

                return loadReponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Adiciona parametros
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="executionHeader"></param>
        /// <param name="trustedHeader"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private async Task<SetExecutionParametersResponse> AddParametersToTheReport(ReportExecutionServiceSoapClient rs, ExecutionHeader executionHeader, TrustedUserHeader trustedHeader, Dictionary<string, string> parametros)
        {
            try
            {
                // Add parameters to the report
                var reportParameters = new List<ParameterValue>();
                foreach (var item in parametros)
                {
                    reportParameters.Add(new ParameterValue() { Name = item.Key, Value = item.Value });
                }

                SetExecutionParametersResponse setParamsResponse = await rs.SetExecutionParametersAsync(executionHeader, trustedHeader, reportParameters.ToArray(), "en-US");

                return setParamsResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// rederizacion del archivo
        /// </summary>
        /// <param name="execHeader"></param>
        /// <param name="trustedHeader"></param>
        /// <param name="rs"></param>
        /// <param name="format"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private async Task<RenderResponse> RenderReportByteArrayAsync(ExecutionHeader execHeader, TrustedUserHeader trustedHeader,
           ReportExecutionServiceSoapClient rs, string format, string width, string height)
        {
            try
            {
                string deviceInfo = String.Format("<DeviceInfo><PageHeight>{0}</PageHeight><PageWidth>{1}</PageWidth><PrintDpiX>300</PrintDpiX><PrintDpiY>300</PrintDpiY></DeviceInfo>", height, width);

                var renderRequest = new RenderRequest(execHeader, trustedHeader, format, deviceInfo);

                //get report bytes
                RenderResponse response = await rs.RenderAsync(renderRequest);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception("Error al crear archivo. Detalles: " + e.Message);
            }

        }

        /// <summary>
        /// acceso a report services
        /// </summary>
        /// <returns></returns>
        private async Task<ReportExecutionServiceSoapClient> CreateClient()
        {
            try
            {
                var rsBinding = new BasicHttpBinding();
                rsBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                rsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;

                // So we can download reports bigger than 64 KBytes
                // See https://stackoverflow.com/questions/884235/wcf-how-to-increase-message-size-quota
                rsBinding.MaxBufferPoolSize = 20000000;
                rsBinding.MaxBufferSize = 20000000;
                rsBinding.MaxReceivedMessageSize = 20000000;
                rsBinding.SendTimeout = new TimeSpan(0, 10, 0);

                var rsEndpointAddress = new EndpointAddress(EndpointUrl);
                var rsClient = new ReportExecutionServiceSoapClient(rsBinding, rsEndpointAddress);

                // Set user name and password
                rsClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                rsClient.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(
                   UserName,
                   Password,
                   Domain);

                return rsClient;
            }
            catch (Exception e)
            {
                throw new Exception("No se realizo la conexion con Sql Server Reporting Server. Detalles: " + e.Message);
            }

        }

        /// <summary>
        /// guarda archivo en el servidor
        /// </summary>
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        private void SaveResultToFile(byte[] result, string fileName)
        {
            try
            {
                using (var fs = File.OpenWrite($"{ruta}\\{fileName}"))
                using (var sw = new StreamWriter(fs))
                {
                    fs.Write(result);
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error al guardar archivo. Detalles: " + e.Message);
            }
        }

    }
}
