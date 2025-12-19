using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class UtakmicaViewModel
    {
        public int UtakmicaID { get; set; }

        [Display(Name = "TurnirID")]
        public int? TurnirID { get; set; }

        [Required(ErrorMessage = "Datum je obavezan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Mesto je obavezno")]
        [Display(Name = "Mesto")]
        public string Mesto { get; set; }

        [Required(ErrorMessage = "Domaći klub je obavezan")]
        [Display(Name = "Domaći klub")]
        public string PrviKlubNaziv { get; set; }

        [Required(ErrorMessage = "Gostujući klub je obavezan")]
        [Display(Name = "Gostujući klub")]
        public string DrugiKlubNaziv { get; set; }

        [Display(Name = "Kolo")]
        public string Kolo { get; set; }

        [Required]
        [Range(0, 250, ErrorMessage = "Golovi ne mogu biti negativni")]
        [Display(Name = "Domaći klub golovi")]
        public int PrviKlubGolovi { get; set; }

        [Required]
        [Range(0, 250, ErrorMessage = "Golovi ne mogu biti negativni")]
        [Display(Name = "Gostujući klub golovi")]
        public int DrugiKlubGolovi { get; set; }

        [Display(Name = "Produžeci")]
        public bool Produzeci { get; set; }

        [Display(Name = "Penali")]
        public bool Penali { get; set; }

        [Display(Name = "Domaći klub penali")]
        public int? PrviKlubPenali { get; set; }

        [Display(Name = "Gostujući klub penali")]
        public int? DrugiKlubPenali { get; set; }
        [Display(Name = "Naziv turnira")]
        public string? NazivTurnira { get; set; }
    }
}