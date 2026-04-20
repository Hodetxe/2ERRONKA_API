using FluentNHibernate.Mapping;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Mapeoak
{
    public class HornitzaileaMap : ClassMap<Hornitzailea>
    {
        public HornitzaileaMap()
        {
            Table("hornitzaileak");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Izena).Column("izena");
            Map(x => x.Kontaktua).Column("kontaktua");
            Map(x => x.Helbidea).Column("helbidea");
        }
    }
}
