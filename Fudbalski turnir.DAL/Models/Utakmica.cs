using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        public DateTime Datum { get; set; }
        [MaxLength(50)]
        public string Mesto { get; set; }
        [MaxLength(50)]
        [Display(Name = "Domaći klub")] // dependecy za ime se nalazi u views/utakmica/create.cshtml
        public string PrviKlubNaziv { get; set; }
        [MaxLength(50)]
        [Display(Name = "Gostujući klub")]
        public string DrugiKlubNaziv { get; set; }
        [MaxLength(20)]
        public string Kolo { get; set; }
        [Display(Name = "Domaći klub golovi")]
        public int PrviKlubGolovi { get; set; }
        [Display(Name = "Gostujući klub golovi")]
        public int DrugiKlubGolovi { get; set; }
        public bool Produzeci { get; set; }
        public bool Penali { get; set; }
        [Display(Name = "Domaći klub penali")]
        public int? PrviKlubPenali { get; set; }
        [Display(Name = "Gostujući klub penali")]
        public int? DrugiKlubPenali { get; set; }
        public ICollection<Klub>? Klubovi { get; set; }

        public Utakmica()
        {

        }
    }
}
