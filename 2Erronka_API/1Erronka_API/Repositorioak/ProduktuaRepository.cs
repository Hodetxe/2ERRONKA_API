using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class ProduktuaRepository
    {
        private readonly NHibernate.ISession _session;

        public ProduktuaRepository(NHibernate.ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }


        public virtual Produktua? Get(int id) =>
            _session.Query<Produktua>().FirstOrDefault(x => x.Id == id);

        public virtual IList<Produktua> GetAll() => _session.Query<Produktua>().ToList();

        public virtual void Add(Produktua produktua)
        {
            using var tx = _session.BeginTransaction();
            _session.Save(produktua);
            tx.Commit();
        }

        public virtual void Update(Produktua produktua)
        {
            using var tx = _session.BeginTransaction();
            _session.Update(produktua);
            tx.Commit();
        }

        public virtual void UpdateStock(int id, int stock)
        {
            using var tx = _session.BeginTransaction();
            var p = _session.Get<Produktua>(id);
            if (p != null) { p.Stock = stock; _session.Update(p); }
            tx.Commit();
        }

        public virtual void Delete(int id)
        {
            using var tx = _session.BeginTransaction();
            var p = _session.Get<Produktua>(id);
            if (p != null) _session.Delete(p);
            tx.Commit();
        }

    }
}
