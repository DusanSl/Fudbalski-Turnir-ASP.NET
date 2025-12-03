namespace Fudbalski_turnir.Models
{
    public class Klub
    {
        public int KlubID { get; set; }
        public string ImeKluba { get; set; }
        public int GodinaOsnivanja { get; set; }
        public int RankingTima { get; set; }
        public int BrojIgraca { get; set; }
        public string Stadion { get; set; }
        public int BrojOsvojenihTitula { get; set; } // MOZDA NIJE POTREBNO
        public ICollection<Utakmica> Utakmice { get; set; } // zbog M:M veze
        public ICollection<Turnir> Turniri { get; set; } // zbog M:M veze

        public Klub()
        {

        }
    }
}
