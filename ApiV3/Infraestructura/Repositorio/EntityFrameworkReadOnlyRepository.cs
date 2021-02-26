using ApiV3.Support;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.Repositorio
{
    public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
          where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly IConfiguration configuration;

        public EntityFrameworkReadOnlyRepository(TContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        private dynamic SqlDataReaderToExpando(DbDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
                expandoObject.Add(reader.GetName(i), reader[i]);

            return expandoObject;
        }

        public virtual IEnumerable<dynamic> Query(string qry)
        //, IEntity
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = qry;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    //dynamic data = new ExpandoObject();

                    while (result.Read())
                    {
                        yield return SqlDataReaderToExpando(result);
                    }
                }
            }
        }

        public Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public bool NonQuery(string qry)
        {
            bool ejecuta = true;
            string conexion = configuration.GetConnectionString(Constants.ApplicationNameConnection);
            using (SqlConnection connection =
                  new SqlConnection(conexion))
            {
                try
                {
                    int count = 0;
                    SqlCommand command = new SqlCommand(qry, connection);
                    connection.Open();
                    IAsyncResult result = command.BeginExecuteNonQuery();
                    while (!result.IsCompleted)
                    {
                        Console.WriteLine("Waiting ({0})", count++);
                        System.Threading.Thread.Sleep(100);
                    }
                    Console.WriteLine("Command complete. Affected {0} rows.",
                    command.EndExecuteNonQuery(result));
                    return ejecuta;
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
