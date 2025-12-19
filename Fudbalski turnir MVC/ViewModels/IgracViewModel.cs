using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class IgracViewModel
    {
        public int OsobaID { get; set; }

        [Required(ErrorMessage = "Klub je obavezan")]
        [Display(Name = "KlubID")]
        public int KlubID { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50, ErrorMessage = "Ime ne može biti duže od 50 karaktera")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(100, ErrorMessage = "Prezime ne može biti duže od 100 karaktera")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Datum rođenja je obavezan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Datum rođenja")]
        public DateTime DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Nacionalnost je obavezna")]
        [StringLength(50)]
        [Display(Name = "Nacionalnost")]
        public string Nacionalnost { get; set; }

        [Required(ErrorMessage = "Pozicija je obavezna")]
        [StringLength(30)]
        [Display(Name = "Pozicija")]
        public string Pozicija { get; set; }

        [Required(ErrorMessage = "Broj dresa je obavezan")]
        [Range(1, 99, ErrorMessage = "Broj dresa mora biti između 1 i 99")]
        [Display(Name = "Broj dresa")]
        public int BrojDresa { get; set; }
        [Display(Name = "Klub")]
        public string? ImeKluba { get; set; }
    }
}