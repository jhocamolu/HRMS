using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Infraestructura.Resultados
{
    public class CommandResult
    {
        private CommandResult() { }
        
        private CommandResult(string failureReason)
        {
            FailureReason = failureReason;
        }

        public string FailureReason { get; }

        public dynamic Data { get; set; }

        public bool IsSuccess => string.IsNullOrWhiteSpace(FailureReason);

        public static CommandResult Success() => new CommandResult();

        public static CommandResult Success(dynamic data)
        {
            return new CommandResult
            {
                Data = data
            };
        }

        public static CommandResult Fail(string message) => new CommandResult(message);
    }
}
