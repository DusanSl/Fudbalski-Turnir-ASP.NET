using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Klub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KlubID { get; set; }
        [MaxLength(50)]
        [Display(Name = "Ime kluba")]
        public string ImeKluba { get; set; }
        [Display(Name = "Godina osnivanja")]
        public int GodinaOsnivanja { get; set; }
        [Display(Name = "Ranking tima")]
        public int RankingTima { get; set; }
        [Display(Name = "Broj igrača")]
        public int BrojIgraca { get; set; }
        [MaxLength(100)]
        public string Stadion { get; set; }
        [Display(Name = "Broj osvojenih titula")]
        public int BrojOsvojenihTitula { get; set; } 
        public ICollection<Utakmica>? Utakmice { get; set; } // zbog M:M veze
        public ICollection<Turnir>? Turniri { get; set; } // zbog M:M veze

        public Klub()
        {

        }
    }
}
