using System;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class MenadzerViewModel
    {
        public int OsobaID { get; set; }
        [Required(ErrorMessage = "Morate odabrati klub")]
        [Display(Name = "Klub")]
        public int KlubID { get; set; }
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime mora imati između 2 i 50 karaktera")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Prezime mora imati između 2 i 100 karaktera")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Datum rođenja je obavezan")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum rođenja")]
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Nacionalnost je obavezna")]
        [MaxLength(50, ErrorMessage = "Nacionalnost ne može biti duža od 50 karaktera")]
        public string Nacionalnost { get; set; }
        [Required(ErrorMessage = "Godine iskustva su obavezne")]
        [Range(0, 100, ErrorMessage = "Godine iskustva moraju biti između 0 i 100")]
        [Display(Name = "Godine iskustva")]
        public int GodineIskustva { get; set; }
    }
}