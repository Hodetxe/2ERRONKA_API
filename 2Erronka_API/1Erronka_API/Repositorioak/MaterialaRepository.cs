using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class MaterialaRepository
    {
        private readonly NHibernate.ISession _session;

        public MaterialaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual Materiala? Get(int id) =>
            _session.Query<Materiala>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Materiala> GetAll() => _session.Query<Materiala>().ToList();

        public virtual void Add(Materiala materiala)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(materiala);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(materiala);
                tx.Commit();
            }
        }

        public virtual void Update(Materiala materiala)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(materiala);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(materiala);
                tx.Commit();
            }
        }

        public virtual void Delete(Materiala materiala)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(materiala);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(materiala);
                tx.Commit();
            }
        }
    }
}
