namespace _1Erronka_API.DTOak
{
    public class MaterialaDto
    {
        public int Id { get; set; }
        public string Izena { get; set; } = string.Empty;
        public double Prezioa { get; set; }
        public int Stock { get; set; }
        public int HornitzaileakId { get; set; }
    }
}
