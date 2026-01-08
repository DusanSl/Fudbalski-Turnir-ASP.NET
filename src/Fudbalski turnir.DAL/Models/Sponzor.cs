using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Sponzor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SponzorID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImeSponzora { get; set; } = string.Empty; 
        [Required]
        [MaxLength(150)]
        public string KontaktSponzora { get; set; } = string.Empty; 
        [Required]
        public decimal VrednostSponzora { get; set; }
        public ICollection<Turnir>? Turniri { get; set; }
        public Sponzor() 
        { 
        
        }
    }
}