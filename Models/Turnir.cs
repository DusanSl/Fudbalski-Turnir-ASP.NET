using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    public class Turnir
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int TurnirID { get; set; }
       [MaxLength(100)]
       public string NazivTurnira { get; set; }
       [MaxLength(50)]
       public string Lokacija { get; set; }
       public DateTime DatumPocetka { get; set; }
       public DateTime DatumZavrsetka { get; set; }
       [MaxLength(50)]
       public string TipTurnira { get; set; }
       public ICollection<Sponzor>? Sponzori { get; set; } // zbog M:M veze
       public ICollection<Klub>? Klubovi { get; set; } // zbog M:M veze
       public ICollection<Utakmica>? Utakmice { get; set; }

        public Turnir()
       {

       }
    }
}
