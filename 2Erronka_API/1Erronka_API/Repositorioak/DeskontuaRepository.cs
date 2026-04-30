using NHibernate;
using _1Erronka_API.Modeloak;
using System.Linq;

namespace _1Erronka_API.Repositorioak
{
    public class DeskontuaRepository
    {
        private readonly NHibernate.ISession _session;

        public DeskontuaRepository(NHibernate.ISessionFactory sessionFactory)
        {
            _session = sessionFactory.GetCurrentSession();
        }

        public virtual Deskontua? GetByKodea(string kodea)
        {
            kodea = (kodea ?? string.Empty).Trim();
            if (kodea.Length == 0) return null;
            return _session.Query<Deskontua>().FirstOrDefault(x => x.Kodea == kodea);
        }

        public virtual void SaveOrUpdate(Deskontua deskontua)
        {
            if (_session.Transaction != null && _session.Transaction.IsActive)
            {
                _session.SaveOrUpdate(deskontua);
            }
            else
            {
                using var tx = _session.BeginTransaction();
                _session.SaveOrUpdate(deskontua);
                tx.Commit();
            }
        }
    }
}
