using System.Collections.Generic;

namespace Plantillas.Infraestructura.Resultados
{
    public class QueryResult
    {
        private QueryResult() { }

        private QueryResult(string failureReason)
        {
            FailureReason = failureReason;
        }

        public string FailureReason { get; }

        public int Page { get; set; }

        public int Count { get; set; }

        public IEnumerable<dynamic> Data { get; set; }

        public bool IsSuccess => string.IsNullOrWhiteSpace(FailureReason);

        public static QueryResult Success(int page, int count, IEnumerable<dynamic> data)
        {
            return new QueryResult
            {
                Page = page,
                Count = count,
                Data = data ?? new List<dynamic>()
            };
        }


        public static QueryResult Fail(string message) => new QueryResult(message);
    }
}
