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
        [Required]
        public string ImeKluba { get; set; } = string.Empty;
        [Required]
        public int GodinaOsnivanja { get; set; }
        [Required]
        public int RankingTima { get; set; }
        [Required]
        public int BrojIgraca { get; set; }
        [Required]
        [MaxLength(100)]
        public string Stadion { get; set; } = string.Empty;
        [Required]
        public int BrojOsvojenihTitula { get; set; }
        public ICollection<Utakmica>? Utakmice { get; set; }
        public ICollection<Turnir>? Turniri { get; set; }
        public Klub()
        {

        }
    }
}