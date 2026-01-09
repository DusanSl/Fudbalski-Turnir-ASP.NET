namespace FudbalskiTurnir.BLL.DTOs
{
    public class SponzorDTO
    {
        public int SponzorID { get; set; }
        public string ImeSponzora { get; set; } = string.Empty;
        public string KontaktSponzora { get; set; } = string.Empty;
        public decimal VrednostSponzora { get; set; }
        public int? PrimarniTurnirID { get; set; }
        public List<string> NaziviTurnira { get; set; } = new List<string>();
    }
}