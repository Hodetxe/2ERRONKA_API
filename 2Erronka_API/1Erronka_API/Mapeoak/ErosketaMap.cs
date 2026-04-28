using FluentNHibernate.Mapping;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Mapeoak
{
    public class ErosketaMap : ClassMap<Erosketa>
    {
        public ErosketaMap()
        {
            Table("erosketa");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.HornitzaileaId).Column("hornitzailea_id");
            Map(x => x.OsagaiaId).Column("osagaia_id").Nullable();
            Map(x => x.Prezioa).Column("prezioa");
            Map(x => x.Kantitatea).Column("kantitatea");
            Map(x => x.MaterialaId).Column("materiala_id").Nullable();
        }
    }
}
