namespace FudbalskiTurnir.BLL.DTOs
{
    public class TurnirDTO
    {
        public int TurnirID { get; set; }
        public string NazivTurnira { get; set; } = string.Empty;
        public string Lokacija { get; set; } = string.Empty;
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string TipTurnira { get; set; } = string.Empty;
        public int BrojUtakmica { get; set; }
    }
}