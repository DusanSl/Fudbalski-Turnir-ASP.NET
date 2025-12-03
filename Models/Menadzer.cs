namespace Fudbalski_turnir.Models
{
    public class Menadzer : Osoba
    {
        public int MenadzerID { get; set; }
        public int KlubID { get; set; }
        public int GodineIskustva { get; set; }

        public Menadzer()
        {

        }
    }
}
