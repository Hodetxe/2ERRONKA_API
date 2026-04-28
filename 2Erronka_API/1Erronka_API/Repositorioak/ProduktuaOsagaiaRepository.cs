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

        public virtual IList<ProduktuaOsagaia> GetAll()
        {
            return _session.Query<ProduktuaOsagaia>().ToList();
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

        public virtual void Add(ProduktuaOsagaia produktuaOsagaia)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(produktuaOsagaia);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(produktuaOsagaia);
                tx.Commit();
            }
        }

        public virtual void Update(ProduktuaOsagaia produktuaOsagaia)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(produktuaOsagaia);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(produktuaOsagaia);
                tx.Commit();
            }
        }

        public virtual void Delete(ProduktuaOsagaia produktuaOsagaia)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(produktuaOsagaia);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(produktuaOsagaia);
                tx.Commit();
            }
        }

        public virtual void UpdateOsagaia(Osagaia osagaia)
        {
            _session.Update(osagaia);
        }
    }
}
