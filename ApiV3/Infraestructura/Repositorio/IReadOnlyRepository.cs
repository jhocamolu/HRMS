using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.Repositorio
{
    public interface IReadOnlyRepository
    {

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class; //, IEntity
        IEnumerable<dynamic> Query(string qry);

        bool NonQuery(string qry);
    }
}
