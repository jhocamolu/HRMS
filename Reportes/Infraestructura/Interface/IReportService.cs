using Reportes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Infraestructura.Interface
{
    public interface IReportService
    {
        Task<string> Ruta(Reporte reporte, Dictionary<string, string> param = null, Dictionary<string, string> options = null);
    }
}
