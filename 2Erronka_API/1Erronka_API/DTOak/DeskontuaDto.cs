namespace _1Erronka_API.DTOak
{
    public class DeskontuaUpsertDto
    {
        public string Kodea { get; set; } = string.Empty;
        public string Mota { get; set; } = string.Empty;
        public double Balioa { get; set; }
        public bool Aktibo { get; set; } = true;
    }
}
