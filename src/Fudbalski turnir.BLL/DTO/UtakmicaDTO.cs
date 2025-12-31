namespace FudbalskiTurnir.BLL.DTOs
{
    public class UtakmicaDTO
    {
        public int UtakmicaID { get; set; }
        public int? TurnirID { get; set; }
        public string? NazivTurnira { get; set; }
        public DateTime Datum { get; set; }
        public string Mesto { get; set; }
        public string PrviKlubNaziv { get; set; }
        public string DrugiKlubNaziv { get; set; }
        public string Kolo { get; set; }
        public int PrviKlubGolovi { get; set; }
        public int DrugiKlubGolovi { get; set; }
        public bool Produzeci { get; set; }
        public bool Penali { get; set; }
        public int? PrviKlubPenali { get; set; }
        public int? DrugiKlubPenali { get; set; }
        public string? Grupa { get; set; }
    }
}