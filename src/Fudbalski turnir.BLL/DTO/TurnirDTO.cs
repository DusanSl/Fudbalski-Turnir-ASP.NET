namespace FudbalskiTurnir.BLL.DTOs
{
    public class TurnirDTO
    {
        public int TurnirID { get; set; }
        public string NazivTurnira { get; set; }
        public string Lokacija { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string TipTurnira { get; set; }
        public int BrojUtakmica { get; set; }
    }
}