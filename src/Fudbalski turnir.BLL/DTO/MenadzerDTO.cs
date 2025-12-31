namespace FudbalskiTurnir.BLL.DTOs
{
    public class MenadzerDTO
    {
        public int OsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Nacionalnost { get; set; }
        public int GodineIskustva { get; set; }
        public int KlubID { get; set; }
        public string? ImeKluba { get; set; } 
    }
}