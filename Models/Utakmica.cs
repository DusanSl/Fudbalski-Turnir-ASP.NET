using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    public class Utakmica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtakmicaID { get; set; }
        public DateTime Datum { get; set; }
        public string Mesto { get; set; }
        public string PrviKlubNaziv { get; set; }
        public string DrugiKlubNaziv { get; set; }
        public string Kolo { get; set; }
        public int PrviKlubGolovi { get; set; }
        public int DrugiKlubGolovi { get; set; }
        public bool Produzeci { get; set; }
        public bool Penali { get; set; }
        public int PrviKlubPenali { get; set; }
        public int DrugiKlubPenali { get; set; }
        public int TipUcesca { get; set; } // gost ili domacin
        public ICollection<Klub> Klubovi { get; set; }

        public Utakmica()
        {

        }
    }
}
