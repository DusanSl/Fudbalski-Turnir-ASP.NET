using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Turnir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TurnirID { get; set; }
        [Required]
        [MaxLength(100)]
        public string NazivTurnira { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Lokacija { get; set; } = string.Empty;
        [Required]
        public DateTime DatumPocetka { get; set; }
        [Required]
        public DateTime DatumZavrsetka { get; set; }
        [MaxLength(50)]
        [Required]
        public string TipTurnira { get; set; } = string.Empty;
        public ICollection<Sponzor>? Sponzori { get; set; }
        public ICollection<Klub>? Klubovi { get; set; }
        public ICollection<Utakmica>? Utakmice { get; set; }
        public Turnir() 
        { 
        
        }
    }
}