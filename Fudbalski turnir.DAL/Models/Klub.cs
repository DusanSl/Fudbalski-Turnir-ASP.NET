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
        public string ImeKluba { get; set; }
        public int GodinaOsnivanja { get; set; }
        public int RankingTima { get; set; }
        public int BrojIgraca { get; set; }
        [MaxLength(100)]
        public string Stadion { get; set; }
        public int BrojOsvojenihTitula { get; set; } 
        public ICollection<Utakmica>? Utakmice { get; set; } // zbog M:M veze
        public ICollection<Turnir>? Turniri { get; set; } // zbog M:M veze

        public Klub()
        {

        }
    }
}
