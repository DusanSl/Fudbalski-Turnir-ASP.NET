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
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [MaxLength(50)]
        public string Mesto { get; set; }
        [MaxLength(50)]
        public string PrviKlubNaziv { get; set; }
        [MaxLength(50)]
        public string DrugiKlubNaziv { get; set; }
        [MaxLength(20)]
        public string Kolo { get; set; }
        public int PrviKlubGolovi { get; set; }
        public int DrugiKlubGolovi { get; set; }
        public bool Produzeci { get; set; }
        public bool Penali { get; set; }
        public int? PrviKlubPenali { get; set; }
        public int? DrugiKlubPenali { get; set; }

        public ICollection<Klub>? Klubovi { get; set; }
    }
}