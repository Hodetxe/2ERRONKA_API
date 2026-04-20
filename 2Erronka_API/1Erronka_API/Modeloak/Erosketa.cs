namespace _1Erronka_API.Modeloak
{
    public class Erosketa
    {
        public virtual int Id { get; set; }
        public virtual int HornitzaileaId { get; set; }
        public virtual int? OsagaiaId { get; set; }
        public virtual double Prezioa { get; set; }
        public virtual int Kantitatea { get; set; }
        public virtual int? MaterialaId { get; set; }
    }
}
