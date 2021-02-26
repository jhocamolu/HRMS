namespace ApiV3.Support
{
    /// <summary>
    /// Constants for integration service.
    /// </summary>
    public static class Constants
    {
        public const string ApplicationName = "NOMINAP";

        public const string ApplicationNameConnection = "AppConnection";

        public static class ClienteMovil
        {
            public const string Key = "ClienteMovil:key";
            public const string Value = "ClienteMovil:Value";
        }
        public static class ServiceApi
        {
            public const string PLANTILLAS = "ApiServicios:plantillas";
            public const string AYUDA = "ApiServicios:ayuda";
            public const string REPORTES = "ApiServicios:reportes";
        }
        public static class ServiceNode
        {
            public const string PDF = "NodeServicios:plantillasPdf";
            public const string ARCHIVOS = "NodeServicios:documentoArchivos";
        }
        public static class Peticion
        {
            public const string LOGIN = "Peticion:Login";
            public const string LOGINAPLICACION = "Peticion:LoginAplicacion";
            public const string PERMISOAPLICACION = "Peticion:PermisoAplicacion";
            public const string LOGOUT = "Peticion:Logout";
            public const string VALIDATIONENDPOINT = "Peticion:ValidationEndpoint";
            public const string TOKENVALIDACION = "Peticion:TokenValidation";
            public const string REFRESHTOKEN = "Peticion:RefreshToken";
            public const string NOMBREAPLICACIONENAUTENTICACION = "Peticion:NombreAplicacionEnAutenticacion";
        }

        public class Documentos
        {
            public const string OBTENERCONTRATO = "Documentos:ObtenerContrato";
        }

        public static class Coneccion
        {
            public const string CONECCION = "connectionStrings:AppConnection";
        }


        public static class ArchivosDesarrollo
        {
            public const string OBTENERARCHIVO = "ArchivosDesarrollo:ObtenerArchivo";
            public const string CREARARCHIVO = "ArchivosDesarrollo:CrearArchivo";
            public const string ELIMINARARCHIVO = "ArchivosDesarrollo:EliminarArchivo";

        }

        public static class ArchivosIntegracion
        {
            public const string OBTENERARCHIVO = "ArchivosIntegracion:ObtenerArchivo";
            public const string CREARARCHIVO = "ArchivosIntegracion:CrearArchivo";
            public const string ELIMINARARCHIVO = "ArchivosIntegracion:EliminarArchivo";

        }

        public static class ArchivosPruebas
        {
            public const string OBTENERARCHIVO = "ArchivosPruebas:ObtenerArchivo";
            public const string CREARARCHIVO = "ArchivosPruebas:CrearArchivo";
            public const string ELIMINARARCHIVO = "ArchivosPruebas:EliminarArchivo";

        }

        public static class CertificadoIntegracion
        {
            public const string SUELDO = "CertificadosIntegracion:Sueldo";
            public const string CONTRATO = "CertificadosIntegracion:Cargo";
            public const string SUELDOCONTRATO = "CertificadosIntegracion:CargoSueldo";
        }

        public static class CertificadoPruebas
        {
            public const string SUELDO = "CertificadosPruebas:Sueldo";
            public const string CONTRATO = "CertificadosPruebas:Cargo";
            public const string SUELDOCONTRATO = "CertificadosPruebas:CargoSueldo";
        }

        public static class ConsolaNomina
        {
            public const string DESARROLLO = "ConsolaNomina:Desarrollo";
            public const string INTEGRACION = "ConsolaNomina:Inregarcion";
            public const string PRUEBAS = "ConsolaNomina:Pruebas";
        }

        public static class AmbineteUrl
        {
            public const string DESARROLLO = "AmbineteUrl:Desarrollo";
            public const string INTEGRACION = "AmbineteUrl:Inregarcion";
            public const string PRUEBAS = "AmbineteUrl:Pruebas";
        }

        public static class InformacionSoftlandSicon
        {
            public const string CuentaContable = "InformacionSoftlandSicom:CuentaContable";
            public const string CentroCosto = "InformacionSoftlandSicom:CentroCosto";
            public const string PeriodoContable = "InformacionSoftlandSicom:PeriodoContable";
            public const string ActividadFuncionario = "InformacionSoftlandSicom:ActividadFuncionario";
        }

        public static class HorasExtras
        {
            public const string Api = "HorasExtras:Api";
            
        }
    }
}
