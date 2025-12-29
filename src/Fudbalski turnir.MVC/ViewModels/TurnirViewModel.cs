using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class TurnirViewModel
    {
        public int TurnirID { get; set; }

        [Required(ErrorMessage = "Naziv turnira je obavezan")]
        [StringLength(100, ErrorMessage = "Naziv ne može biti duži od 100 karaktera")]
        [Display(Name = "Naziv turnira")]
        public string NazivTurnira { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lokacija je obavezna")]
        [StringLength(50, ErrorMessage = "Lokacija ne može biti duža od 50 karaktera")]
        public string Lokacija { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum početka je obavezan")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum početka")]
        public DateTime DatumPocetka { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Datum završetka je obavezan")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum završetka")]
        public DateTime DatumZavrsetka { get; set; } = DateTime.Now.AddDays(30);

        [Required(ErrorMessage = "Tip turnira je obavezan")]
        [StringLength(50, ErrorMessage = "Tip ne može biti duži od 50 karaktera")]
        [Display(Name = "Tip turnira")]
        public string TipTurnira { get; set; } = string.Empty;
    }
}