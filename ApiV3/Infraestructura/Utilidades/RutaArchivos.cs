using ApiV3.Infraestructura.Enumerador;
using ApiV3.Support;

namespace ApiV3.Infraestructura.Utilidades
{
    public class RutaArchivos
    {
        public static string Ambiente()
        {
            return Constants.Coneccion.CONECCION;
        }

        public static string UrlArchivos(string ambiente, NombreArchivo nobrearchivo)
        {
            var obtenerArchivo = "";
            var crearArchivo = "";
            var eliminarArchivo = "";
            if (ambiente.Equals("DESARROLLO"))
            {
                obtenerArchivo = Constants.ArchivosDesarrollo.OBTENERARCHIVO;
                crearArchivo = Constants.ArchivosDesarrollo.CREARARCHIVO;
                eliminarArchivo = Constants.ArchivosDesarrollo.ELIMINARARCHIVO;
            }
            else if (ambiente.Equals("INTEGRACION"))
            {
                obtenerArchivo = Constants.ArchivosIntegracion.OBTENERARCHIVO;
                crearArchivo = Constants.ArchivosIntegracion.CREARARCHIVO;
                eliminarArchivo = Constants.ArchivosIntegracion.ELIMINARARCHIVO;
            }
            else if (ambiente.Equals("PRUEBAS"))
            {
                obtenerArchivo = Constants.ArchivosPruebas.OBTENERARCHIVO;
                crearArchivo = Constants.ArchivosPruebas.CREARARCHIVO;
                eliminarArchivo = Constants.ArchivosPruebas.ELIMINARARCHIVO;
            }


            if (nobrearchivo.Equals(NombreArchivo.OBTENERARCHIVO))
            {
                return obtenerArchivo;
            }
            else if (nobrearchivo.Equals(NombreArchivo.CREARARCHIVO))
            {
                return crearArchivo;
            }
            else if (nobrearchivo.Equals(NombreArchivo.ELIMINARARCHIVO))
            {
                return eliminarArchivo;
            }
            return "Nombre Archivo No valido";
        }

        public static string UrlCertificado(string ambiente, TipoCertificado nobrearchivo)
        {
            var sueldo = "";
            var cargo = "";
            var sueldocontrato = "";
            if (ambiente.Equals("DESARROLLO") || ambiente.Equals("INTEGRACION"))
            {
                sueldo = Constants.CertificadoIntegracion.SUELDO;
                cargo = Constants.CertificadoIntegracion.CONTRATO;
                sueldocontrato = Constants.CertificadoIntegracion.SUELDOCONTRATO;
            }
            else if (ambiente.Equals("PRUEBAS"))
            {
                sueldo = Constants.CertificadoPruebas.SUELDO;
                cargo = Constants.CertificadoPruebas.CONTRATO;
                sueldocontrato = Constants.CertificadoPruebas.SUELDOCONTRATO;
            }


            if (nobrearchivo.Equals(TipoCertificado.SUELDO))
            {
                return sueldo;
            }
            else if (nobrearchivo.Equals(TipoCertificado.CARGO))
            {
                return cargo;
            }
            else if (nobrearchivo.Equals(TipoCertificado.SUELDOCARGO))
            {
                return sueldocontrato;
            }
            return "Nombre Archivo No valido";
        }

        public static string UrlNominaConsola(string ambiente)
        {
            string url = "";
            if (ambiente.Equals("DESARROLLO"))
            {
                url = Constants.ConsolaNomina.DESARROLLO;
            }
            else if (ambiente.Equals("INTEGRACION"))
            {
                url = Constants.ConsolaNomina.INTEGRACION;
            }
            else if (ambiente.Equals("PRUEBAS"))
            {
                url = Constants.ConsolaNomina.PRUEBAS;
            }
            return url;
        }

        public static string UrlAmbinete(string ambiente)
        {
            string url = "";
            if (ambiente.Equals("DESARROLLO"))
            {
                url = Constants.AmbineteUrl.DESARROLLO;
            }
            else if (ambiente.Equals("INTEGRACION"))
            {
                url = Constants.AmbineteUrl.INTEGRACION;
            }
            else if (ambiente.Equals("PRUEBAS"))
            {
                url = Constants.AmbineteUrl.PRUEBAS;
            }
            return url;
        }
    }
}
