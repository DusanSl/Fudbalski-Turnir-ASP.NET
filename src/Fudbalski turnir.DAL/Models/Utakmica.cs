using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    public class Utakmica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtakmicaID { get; set; }
        public int? TurnirID { get; set; }
        [ForeignKey("TurnirID")]
        public Turnir? Turnir { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        [MaxLength(50)]
        public string Mesto { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string PrviKlubNaziv { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string DrugiKlubNaziv { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Kolo { get; set; } = string.Empty;
        [Required]
        public int PrviKlubGolovi { get; set; }
        [Required]
        public int DrugiKlubGolovi { get; set; }
        [Required]
        public bool Produzeci { get; set; }
        [Required]
        public bool Penali { get; set; }
        public int? PrviKlubPenali { get; set; }
        public int? DrugiKlubPenali { get; set; }
        [MaxLength(20)]
        public string? Grupa { get; set; }
        public ICollection<Klub>? Klubovi { get; set; }
    }
}