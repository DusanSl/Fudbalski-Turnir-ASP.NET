using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    public class Sponzor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SponzorID { get; set; }
        public string ImeSponzora { get; set; }
        public string KontaktSponzora { get; set; }
        public decimal VrednostSponzora { get; set; } 
        public ICollection<Turnir>? Turniri { get; set; } // zbog M:M veze

        public Sponzor()
        {

        }
    }
}
