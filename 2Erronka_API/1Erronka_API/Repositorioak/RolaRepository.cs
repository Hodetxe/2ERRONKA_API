using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class RolaRepository
    {
        private readonly NHibernate.ISession _session;

        public RolaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual Rola? Get(int id) =>
            _session.Query<Rola>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Rola> GetAll() => _session.Query<Rola>().ToList();

        public virtual void Add(Rola rola)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(rola);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(rola);
                tx.Commit();
            }
        }

        public virtual void Update(Rola rola)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(rola);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(rola);
                tx.Commit();
            }
        }

        public virtual void Delete(Rola rola)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(rola);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(rola);
                tx.Commit();
            }
        }
    }
}
