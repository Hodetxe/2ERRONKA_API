using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class OsagaiaRepository
    {
        private readonly NHibernate.ISession _session;

        public OsagaiaRepository(NHibernate.ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }


        public virtual Osagaia? Get(int id) =>
            _session.Query<Osagaia>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Osagaia> GetAll() => _session.Query<Osagaia>().ToList();

        public virtual void Update(Osagaia osagaia)
        {
            using var tx = _session.BeginTransaction();
            _session.Update(osagaia);
            tx.Commit();
        }

        public virtual void UpdateStock(int id, int stock)
        {
            using var tx = _session.BeginTransaction();
            var o = _session.Get<Osagaia>(id);
            if (o != null) { o.Stock = stock; _session.Update(o); }
            tx.Commit();
        }
    }
}
