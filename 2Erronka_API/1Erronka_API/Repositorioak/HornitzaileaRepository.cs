using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class HornitzaileaRepository
    {
        private readonly NHibernate.ISession _session;

        public HornitzaileaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual Hornitzailea? Get(int id) =>
            _session.Query<Hornitzailea>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Hornitzailea> GetAll() => _session.Query<Hornitzailea>().ToList();

        public virtual void Add(Hornitzailea hornitzailea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(hornitzailea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(hornitzailea);
                tx.Commit();
            }
        }

        public virtual void Update(Hornitzailea hornitzailea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(hornitzailea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(hornitzailea);
                tx.Commit();
            }
        }

        public virtual void Delete(Hornitzailea hornitzailea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(hornitzailea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(hornitzailea);
                tx.Commit();
            }
        }
    }
}
