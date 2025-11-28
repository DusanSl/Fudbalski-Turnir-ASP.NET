namespace Fudbalski_turnir.Models
{
    public class Igra
    {
        public int KlubID { get; set; }
        public int UtakmicaID { get; set; }

        public Klub Klub { get; set; }
        public Utakmica Utakmica { get; set; }
    }
}
