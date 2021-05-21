using NHibernate;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.HIbernate {
    internal class NHibernateDbSession : INHibernateDbSession {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateDbSession(ISession session) {
            _session = session;
        }

        public IQueryable<T> Get<T>() => _session.Query<T>();

        public void BeginTransaction() {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit() {
            await _transaction.CommitAsync();
        }

        public async Task Rollback() {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction() {
            if (_transaction != null) {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Save<T>(T entity) {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete<T>(T entity) {
            await _session.DeleteAsync(entity);
        }
    }
}
