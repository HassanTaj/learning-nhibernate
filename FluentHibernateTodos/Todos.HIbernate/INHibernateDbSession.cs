using System.Linq;
using System.Threading.Tasks;

namespace Todos.HIbernate {
    public interface INHibernateDbSession {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save<T>(T entity);
        Task Delete<T>(T entity);
        IQueryable<T> Get<T>();
    }
}
