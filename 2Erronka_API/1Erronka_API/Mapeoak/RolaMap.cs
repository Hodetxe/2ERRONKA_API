using FluentNHibernate.Mapping;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Mapeoak
{
    public class RolaMap : ClassMap<Rola>
    {
        public RolaMap()
        {
            Table("rolak");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Izena).Column("izena");
        }
    }
}
