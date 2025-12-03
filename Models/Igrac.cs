namespace Fudbalski_turnir.Models
{
    public class Igrac : Osoba
    {
        public int IgracID { get; set; }
        public int KlubID { get; set; }
        public string Pozicija { get; set; }
        public int BrojDresa { get; set; }
        
        public Igrac()
        {

        }
    }
}
