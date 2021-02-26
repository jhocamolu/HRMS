namespace Plantillas.Infraestructura.Resultados
{
    public class CommandResult
    {
        private CommandResult() { }

        private CommandResult(string failureReason, int code)
        {
            this.FailureReason = failureReason;
            this.Code = code;
        }

        public string FailureReason { get; }

        public int? Code { get; }

        public dynamic Data { get; set; }

        public bool IsSuccess => this.Code == null;

        public static CommandResult Success() => new CommandResult();

        public static CommandResult Success(dynamic data)
        {
            return new CommandResult
            {
                Data = data
            };
        }

        public static CommandResult Fail(string message) => new CommandResult(message, 500);

        public static CommandResult Fail(string message, int code) => new CommandResult(message, code);

    }
}
