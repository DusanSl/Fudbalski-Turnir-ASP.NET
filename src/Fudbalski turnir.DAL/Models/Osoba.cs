using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Osoba
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OsobaID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Prezime { get; set; } = string.Empty;
        [Required]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nacionalnost { get; set; } = string.Empty;
        public Osoba()
        {

        }
    }
}
