using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class UtakmicaViewModel : IValidatableObject
    {
        public int UtakmicaID { get; set; }

        [Required(ErrorMessage = "Morate izabrati turnir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati turnir.")]
        [Display(Name = "TurnirID")]
        public int? TurnirID { get; set; }

        [Required(ErrorMessage = "Datum je obavezan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Mesto je obavezno")]
        [Display(Name = "Mesto")]
        public string Mesto { get; set; }

        [Required(ErrorMessage = "Domaći klub je obavezan")]
        [Display(Name = "Domaći klub")]
        public string PrviKlubNaziv { get; set; }

        [Required(ErrorMessage = "Gostujući klub je obavezan")]
        [Display(Name = "Gostujući klub")]
        public string DrugiKlubNaziv { get; set; }

        [Required(ErrorMessage = "Kolo je obavezno")]
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
        public string? Grupa { get; set; } 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(PrviKlubNaziv) &&
                !string.IsNullOrEmpty(DrugiKlubNaziv) &&
                PrviKlubNaziv == DrugiKlubNaziv)
            {
                yield return new ValidationResult(
                    "Domaći i gostujući klub ne mogu biti isti.",
                    new[] { nameof(DrugiKlubNaziv) }
                );
            }
        }
    }
}