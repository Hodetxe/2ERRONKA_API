using NHibernate;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Repositorioak
{
    public class EskariaProduktuaRepository
    {
        private readonly NHibernate.ISession _session;

        public EskariaProduktuaRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual IList<EskariaProduktua> GetAll()
        {
            return _session.Query<EskariaProduktua>().ToList();
        }

        public virtual EskariaProduktua? Get(int eskariaId, int produktuaId)
        {
            return _session.Query<EskariaProduktua>()
                .FirstOrDefault(ep => ep.Eskaria.Id == eskariaId && ep.Produktua.Id == produktuaId);
        }

        public virtual void Add(EskariaProduktua eskariaProduktua)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Save(eskariaProduktua);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Save(eskariaProduktua);
                tx.Commit();
            }
        }

        public virtual void Update(EskariaProduktua eskariaProduktua)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Update(eskariaProduktua);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Update(eskariaProduktua);
                tx.Commit();
            }
        }

        public virtual void Delete(EskariaProduktua eskariaProduktua)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.Delete(eskariaProduktua);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.Delete(eskariaProduktua);
                tx.Commit();
            }
        }
    }
}
