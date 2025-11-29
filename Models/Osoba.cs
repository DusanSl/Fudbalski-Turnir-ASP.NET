namespace Fudbalski_turnir.Models
{
    public class Osoba
    {
        public int OsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Nacionalnost { get; set; }
        public DateOnly UKlubuOd { get; set; } // klub u kojem osoba trenutno igra ili radi
        public DateOnly UKlubuDo { get; set; } 
    }
}
