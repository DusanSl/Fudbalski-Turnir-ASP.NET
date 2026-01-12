using FudbalskiTurnir.BLL.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FudbalskiTurnir.ViewModels
{
    public class SponzorViewModel
    {
        public int SponzorID { get; set; }

        [Required(ErrorMessage = "Morate izabrati turnir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati turnir.")]
        [Display(Name = "Turnir")]
        public int? TurnirID { get; set; }

        [Required(ErrorMessage = "Ime sponzora je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime sponzora mora imati između 2 i 50 karaktera")]
        [Display(Name = "Ime sponzora")]
        public string ImeSponzora { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontakt sponzora je obavezan")]
        [StringLength(150, ErrorMessage = "Kontakt ne može biti duži od 150 karaktera")]
        [Display(Name = "Kontakt sponzora")]
        public string KontaktSponzora { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vrednost sponzora je obavezna")]
        [Range(1, 1000000000, ErrorMessage = "Vrednost mora biti veća od 0")]
        [Display(Name = "Vrednost sponzora (€)")]
        public decimal VrednostSponzora { get; set; }

        [Display(Name = "Turnir")]
        public string? NazivTurnira { get; set; }
        public ICollection<TurnirDTO>? Turniri { get; set; }
    }
}