using ApiV3.Servicios.Autenticacion;
using ApiV3.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiV3.Infraestructura.Filtros
{
    public class JwtValidationActionFilter : IActionFilter
    {
        private readonly IAutenticacionService autService;
        private readonly IConfiguration configuration;
        private readonly string value;

        public JwtValidationActionFilter(IAutenticacionService autService, IConfiguration configuration, string value = null)
        {
            this.autService = autService ?? throw new ArgumentNullException(nameof(autService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.value = value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var validarToken = configuration.GetValue<bool>(Constants.Peticion.TOKENVALIDACION);
            if (!validarToken)
                return;

            var hasToken = context.HttpContext.Request.Headers.TryGetValue("JwtToken", out var token);
            if (!hasToken)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                try
                {
                    if (!autService.TokenValido(token, value))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                catch (Exception)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

    }
}

