using ApiV3.Models;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.ProcedimientoDinamicos
{
    public interface IDynamicProcedure
    {
        Task<ConceptoNomina> StoredProcedure(ConceptoNomina conceptoProcedimiento, string formula);
    }
}
