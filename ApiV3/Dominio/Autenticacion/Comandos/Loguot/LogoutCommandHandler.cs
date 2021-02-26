using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Autenticacion.Comandos.Loguot
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Boolean>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;

        public LogoutCommandHandler(IPeticionService peticionService, IConfiguration configuration)
        {
            this.peticionService = peticionService ?? throw new ArgumentNullException(nameof(peticionService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<Boolean> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var url = configuration.GetValue<string>(GetLogout());
            var response = await peticionService.Post(url, request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var usuapp = Convert.ToBoolean(content);
                return usuapp;
            }
            else
            {
                return false;
            }
        }

        private static string GetLogout()
        {
            return Constants.Peticion.LOGOUT;
        }
    }
}
