using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class ProduktuaOsagaiaRepository
    {
        private readonly NHibernate.ISession _session;

        public ProduktuaOsagaiaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual IList<ProduktuaOsagaia> GetByProduktuaId(int produktuaId)
        {
            return _session.Query<ProduktuaOsagaia>()
                .Where(po => po.Produktua.Id == produktuaId)
                .ToList();
        }

        public virtual ProduktuaOsagaia? Get(int produktuaId, int osagaiaId)
        {
            return _session.Query<ProduktuaOsagaia>()
                .FirstOrDefault(po => po.Produktua.Id == produktuaId && po.Osagaia.Id == osagaiaId);
        }

        public virtual void Add(ProduktuaOsagaia po)
        {
            using var tx = _session.BeginTransaction();
            _session.Save(po);
            tx.Commit();
        }

        public virtual void Update(ProduktuaOsagaia po)
        {
            using var tx = _session.BeginTransaction();
            _session.Update(po);
            tx.Commit();
        }

        public virtual void Delete(ProduktuaOsagaia po)
        {
            using var tx = _session.BeginTransaction();
            _session.Delete(po);
            tx.Commit();
        }

        public virtual void UpdateOsagaia(Osagaia osagaia)
        {
            _session.Update(osagaia);
        }
    }
}
