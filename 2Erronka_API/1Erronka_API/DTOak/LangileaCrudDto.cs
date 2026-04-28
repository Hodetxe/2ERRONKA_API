namespace _1Erronka_API.DTOak
{
    public class LangileaCrudDto
    {
        public int Id { get; set; }
        public string Izena { get; set; } = string.Empty;
        public string Abizena { get; set; } = string.Empty;
        public string Erabiltzaile_izena { get; set; } = string.Empty;
        public int Langile_kodea { get; set; }
        public string Pasahitza { get; set; } = string.Empty;
        public int RolaId { get; set; }
        public bool Ezabatua { get; set; }
        public bool Chat { get; set; }
    }
}
