using FluentNHibernate.Mapping;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Mapeoak
{
    public class MaterialaMap : ClassMap<Materiala>
    {
        public MaterialaMap()
        {
            Table("materialak");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Izena).Column("izena");
            Map(x => x.Prezioa).Column("prezioa");
            Map(x => x.Stock).Column("stock");
            Map(x => x.HornitzaileakId).Column("hornitzaileak_id");
        }
    }
}
