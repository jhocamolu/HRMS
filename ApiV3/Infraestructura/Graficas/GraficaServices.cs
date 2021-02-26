using BeautifulColors;
using System;
using System.Collections.Generic;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.Graficas
{
    public class GraficaServices : IGraficaServices
    {

        public object GraficaBasica(string type, string name, object label, object datas, string footerTitle, string footerValue, object option)
        {
            try
            {
                List<Colores> Colores = new List<Colores>();
                Colores.AddRange(this.ColoresChart());

                object grafica = new
                {
                    Type = type,
                    labels = label,
                    data = new
                    {
                        label = name,
                        data = datas,
                        backgroundColor = Colores.Where(x => x.Grafica == type).OrderBy(x => x.Orden).Select(x => x.Rbga).ToList(),
                        hoverBackgroundColor = Colores.Where(x => x.Grafica == type).OrderBy(x => x.Orden).Select(x => x.HoverRbga).ToList()
                    },
                    footerTitle = footerTitle,
                    footerValue = footerValue,
                    Options = option
                };

                return grafica;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public object Widget(string name, string dataCount, string color, string extraCount, object data)
        {
            try
            {
                var colorDinamico = new ColorFactory();

                string colorFijo = string.IsNullOrEmpty(color) ? colorDinamico.RandomBeautiful().Select(c => c.ToString("HEX", null)).ToString() : color;
                object objeto = new
                {
                    label = name,
                    count = dataCount,
                    dataModal = data,
                    color = colorFijo,
                    extra =
                  new
                  {
                      count = extraCount
                  }
                };
                return objeto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<Colores> ColoresChart()
        {
            List<Colores> Colores = new List<Colores>();
            // Colores para grafica de barras
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#EE564C", Rbga = "RGBA(238,86,76,1)", HoverRbga = "RGBA(238,86,76,0.5)", Orden = 1 }
                );
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#EF6100", Rbga = "RGBA(239,97,0,1)", HoverRbga = "RGBA(239,97,0,0.5)", Orden = 2 }
                );
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#3FD195", Rbga = "RGBA(63,209,149,1)", HoverRbga = "RGBA(63,209,149,0.5)", Orden = 3 }
                );
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#3DBDD3", Rbga = "RGBA(61,189,211,1)", HoverRbga = "RGBA(61,189,211,0.5)", Orden = 4 }
                );
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#FF7D7D", Rbga = "RGBA(255,125,125,1)", HoverRbga = "RGBA(255,125,125,0.5)", Orden = 5 }
                );
            Colores.Add(
                new Colores { Grafica = "bar", Hexadecimal = "#FFA124", Rbga = "RGBA(255,161,36,1)", HoverRbga = "RGBA(255,161,36,0.5)", Orden = 6 }
                );

            // Colores para la grafica de Dona
            Colores.Add(
                new Colores { Grafica = "doughnut", Hexadecimal = "#6232CC", Rbga = "RGBA(98,50,205,1)", HoverRbga = "RGBA(98,50,205,0.5)", Orden = 1 }
                );

            Colores.Add(
                new Colores { Grafica = "doughnut", Hexadecimal = "#FFA124", Rbga = "RGBA(255,161,36,1)", HoverRbga = "RGBA(255,161,36,0.5)", Orden = 2 }
                );

            Colores.Add(
                new Colores { Grafica = "doughnut", Hexadecimal = "#066F77", Rbga = "RGBA(6,111,119,1)", HoverRbga = "RGBA(6,111,119,0.5)", Orden = 3 }
                );
            Colores.Add(
                new Colores { Grafica = "doughnut", Hexadecimal = "#B72974", Rbga = "RGBA(183,41,116,1)", HoverRbga = "RGBA(183,41,116,0.5)", Orden = 4 }
                );

            return Colores;
        }
    }
    public class Colores
    {
        public string Grafica { get; set; }
        public int Orden { get; set; }
        public string Hexadecimal { get; set; }
        public string Rbga { get; set; }
        public string HoverRbga { get; set; }
    }
}
