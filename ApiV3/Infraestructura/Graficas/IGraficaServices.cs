/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.Graficas
{
    public interface IGraficaServices
    {
        object GraficaBasica(string type, string name, object label, object datas, string footerTitle = null, string footerValue = null, object option = null);

        object Widget(string name, string dataCount, string color, string extraCount, object dataModal = null);
    }
}
