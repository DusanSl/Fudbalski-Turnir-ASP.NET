using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Sponzor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SponzorID { get; set; }
        [MaxLength(50)]
        [Display(Name = "Ime sponzora")]
        public string ImeSponzora { get; set; }
        [MaxLength(150)]
        [Display(Name = "Kontakt sponzora")]
        public string KontaktSponzora { get; set; }
        [Display(Name = "Vrednost sponzora")]
        public decimal VrednostSponzora { get; set; } 
        public ICollection<Turnir>? Turniri { get; set; } // zbog M:M veze

        public Sponzor()
        {

        }
    }
}
