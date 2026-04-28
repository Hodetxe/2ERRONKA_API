using NHibernate;
using _1Erronka_API.Domain;

namespace _1Erronka_API.Repositorioak
{
    public class LangileaRepository
    {
        private readonly NHibernate.ISession _session;

        public LangileaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual void Add(Langilea langilea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(langilea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(langilea);
                tx.Commit();
            }
        }

        public virtual void Update(Langilea langilea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(langilea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(langilea);
                tx.Commit();
            }
        }

        public virtual void Delete(Langilea langilea)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(langilea);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(langilea);
                tx.Commit();
            }
        }

        public virtual Langilea? Get(int id)
        {
            return _session.Query<Langilea>().SingleOrDefault(x => x.Id == id);
        }

        public virtual IList<Langilea> GetAll()
        {
            return _session.Query<Langilea>().ToList();
        }

    }
}
