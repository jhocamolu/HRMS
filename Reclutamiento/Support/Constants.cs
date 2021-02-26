using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Support
{
    public static class Constants
    {
        public const string ApplicationName = "Api Reclutamiento";

        public const string ApplicationNameConnection = "AppConnection";

        public const string UriEnviroment = "uriEnviroment";

        public const string Ambiente = "ambiente";

        public static class JWT
        {
            public const string KEY = "JWT:key";
        }

        public static class UsuarioPortal
        {
            public const string CEDULA = "UsuarioPortal:Cedula";
            public const string CLAVE = "UsuarioPortal:Clave";
        }
        public static class ServiceApi
        {
            public const string GHESTIC = "ApiServicios:ghestic";
        }
    }
}
