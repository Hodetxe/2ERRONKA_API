using FluentNHibernate.Mapping;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Mapeoak
{
    public class DeskontuaMap : ClassMap<Deskontua>
    {
        public DeskontuaMap()
        {
            Table("deskontuak");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Kodea).Column("kodea").Not.Nullable().Length(60);
            Map(x => x.Mota).Column("mota").Not.Nullable().Length(30);
            Map(x => x.Balioa).Column("balioa").Not.Nullable();
            Map(x => x.Aktibo).Column("aktibo").Not.Nullable();
        }
    }
}
