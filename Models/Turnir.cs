using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    public class Turnir
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int TurnirID { get; set; }
       public string NazivTurnira { get; set; }
       public string Lokacija { get; set; }
       public DateTime DatumPocetka { get; set; }
       public DateTime DatumZavrsetka { get; set; }
       public string TipTurnira { get; set; }
       public ICollection<Sponzor>? Sponzori { get; set; } // zbog M:M veze
       public ICollection<Klub>? Klubovi { get; set; } // zbog M:M veze

       public Turnir()
       {

       }
    }
}
