using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Turnir
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int TurnirID { get; set; }
       [MaxLength(100)]
       [Display(Name = "Naziv turnira")]
       public string NazivTurnira { get; set; }
       [MaxLength(50)]
       public string Lokacija { get; set; }
       [Display(Name = "Datum početka")]
       public DateTime DatumPocetka { get; set; }
       [Display(Name = "Datum završetka")]
       public DateTime DatumZavrsetka { get; set; }
       [MaxLength(50)]
       [Display(Name = "Tip turnira")]
       public string TipTurnira { get; set; }
       public ICollection<Sponzor>? Sponzori { get; set; } // zbog M:M veze
       public ICollection<Klub>? Klubovi { get; set; } // zbog M:M veze
       public ICollection<Utakmica>? Utakmice { get; set; }

        public Turnir()
       {

       }
    }
}
