using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using _1Erronka_API.Mapeoak;
using _1Erronka_API.Modeloak;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace _1Erronka_API
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static NHibernate.ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static ISessionFactory SessionFactory =>
            _sessionFactory ??= CreateSessionFactory();

        private static ISessionFactory CreateSessionFactory()
        {
            string server = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost";
            string port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
            string database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "2erronkaproba";
            string user = Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root";
            string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "";
            string connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};";

            var config = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                .ConnectionString(connectionString))
                .Mappings(m =>
                {
                    m.FluentMappings.Add<ErosketaMap>();
                    m.FluentMappings.Add<ErreserbaMap>();
                    m.FluentMappings.Add<EskariaMap>();
                    m.FluentMappings.Add<EskariaProduktuaMap>();
                    m.FluentMappings.Add<HornitzaileaMap>();
                    m.FluentMappings.Add<LangileaMap>();
                    m.FluentMappings.Add<MahaiaMap>();
                    m.FluentMappings.Add<MaterialaMap>();
                    m.FluentMappings.Add<OsagaiaMap>();
                    m.FluentMappings.Add<ProduktuaMap>();
                    m.FluentMappings.Add<ProduktuaOsagaiaMap>();
                    m.FluentMappings.Add<RolaMap>();
                })
                .ExposeConfiguration(cfg =>
                {
                    cfg.SetProperty("current_session_context_class", "async_local");
                })
                .BuildConfiguration();

            dbEguneratu(config);

                return config.BuildSessionFactory();
        }

        public static void dbEguneratu(NHibernate.Cfg.Configuration config)
        {
            //Eguneratu

            SchemaUpdate schemaUpdate = new SchemaUpdate(config);
            schemaUpdate.Execute(false, true);

        }       
    }
}
