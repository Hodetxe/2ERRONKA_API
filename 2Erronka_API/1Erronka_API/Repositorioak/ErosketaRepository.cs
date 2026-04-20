using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class ErosketaRepository
    {
        private readonly NHibernate.ISession _session;

        public ErosketaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual Erosketa? Get(int id) =>
            _session.Query<Erosketa>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Erosketa> GetAll() => _session.Query<Erosketa>().ToList();

        public virtual void Add(Erosketa erosketa)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(erosketa);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(erosketa);
                tx.Commit();
            }
        }

        public virtual void Update(Erosketa erosketa)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(erosketa);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(erosketa);
                tx.Commit();
            }
        }

        public virtual void Delete(Erosketa erosketa)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(erosketa);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(erosketa);
                tx.Commit();
            }
        }
    }
}
