namespace Fudbalski_turnir.Models
{
    public class Ucestvuje
    {
        public int TurnirID { get; set; }
        public int SponzorID { get; set; }
        public Turnir Turnir { get; set; }
        public Sponzor Sponzor { get; set; }
    }
}
