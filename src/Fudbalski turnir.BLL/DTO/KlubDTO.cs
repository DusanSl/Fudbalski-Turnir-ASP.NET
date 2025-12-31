namespace FudbalskiTurnir.BLL.DTOs
{
    public class KlubDTO
    {
        public int KlubID { get; set; }
        public string ImeKluba { get; set; } = string.Empty;
        public int GodinaOsnivanja { get; set; }
        public int RankingTima { get; set; }
        public int BrojIgraca { get; set; }
        public string Stadion { get; set; } = string.Empty;
        public int BrojOsvojenihTitula { get; set; }
        public List<string> NazivTurnira { get; set; } = new();
        public int? PrimarniTurnirID { get; set; }
    }
}