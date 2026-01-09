using System;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class MenadzerViewModel
    {
        public int OsobaID { get; set; }
        [Required(ErrorMessage = "Morate izabrati klub.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati klub.")]
        [Display(Name = "KlubID")]
        public int KlubID { get; set; }
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime mora imati između 2 i 50 karaktera")]
        [Display(Name = "Ime")]
        public string Ime { get; set; } = string.Empty;
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Prezime mora imati između 2 i 100 karaktera")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; } = string.Empty;
        [Required(ErrorMessage = "Datum rođenja je obavezan")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DatumRodjenja { get; set; } = new DateTime(1980, 1, 1);
        [Required(ErrorMessage = "Nacionalnost je obavezna")]
        [MaxLength(50, ErrorMessage = "Nacionalnost ne može biti duža od 50 karaktera")]
        public string Nacionalnost { get; set; } = string.Empty;
        [Required(ErrorMessage = "Godine iskustva su obavezne")]
        [Range(0, 100, ErrorMessage = "Godine iskustva moraju biti između 0 i 100")]
        [Display(Name = "Godine iskustva")]
        public int GodineIskustva { get; set; }
        [Display(Name = "Ime kluba")]
        public string? ImeKluba { get; set; }
    }
}