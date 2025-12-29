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
        public string ImeSponzora { get; set; } = string.Empty;
        [MaxLength(150)]
        public string KontaktSponzora { get; set; } = string.Empty;
        public decimal VrednostSponzora { get; set; }
        public ICollection<Turnir>? Turniri { get; set; }
        public Sponzor() 
        { 
        
        }
    }
}