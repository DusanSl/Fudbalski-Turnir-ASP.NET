namespace Fudbalski_turnir.Models
{
    public class Sponzorise
    {
        public int SponzorID { get; set; }
        public int TurnirID { get; set; }
        public Sponzor Sponzor { get; set; }
        public Turnir Turnir { get; set; }
    }
}
