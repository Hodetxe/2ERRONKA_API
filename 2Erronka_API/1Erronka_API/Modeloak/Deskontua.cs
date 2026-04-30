namespace _1Erronka_API.Modeloak
{
    public class Deskontua
    {
        public virtual int Id { get; set; }
        public virtual string Kodea { get; set; } = string.Empty;
        public virtual string Mota { get; set; } = string.Empty;
        public virtual double Balioa { get; set; }
        public virtual bool Aktibo { get; set; } = true;
    }
}
