using System.ComponentModel.DataAnnotations;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.ViewModels
{
    public class KlubViewModel
    {
        public int KlubID { get; set; }

        [Required(ErrorMessage = "Ime kluba je obavezno")]
        [StringLength(50, ErrorMessage = "Ime kluba ne može biti duže od 50 karaktera")]
        [Display(Name = "Ime kluba")]
        public string ImeKluba { get; set; }

        [Required(ErrorMessage = "Godina osnivanja je obavezna")]
        [Range(1800, 2100, ErrorMessage = "Godina osnivanja mora biti između 1800 i 2100")]
        [Display(Name = "Godina osnivanja")]
        public int GodinaOsnivanja { get; set; }

        [Required(ErrorMessage = "Ranking kluba je obavezan")]
        [Range(1, 10000, ErrorMessage = "Ranking mora biti između 1 i 10000")]
        [Display(Name = "Ranking kluba")]
        public int RankingTima { get; set; }

        [Required(ErrorMessage = "Broj igrača je obavezan")]
        [Range(0, 30, ErrorMessage = "Broj igrača mora biti između 0 i 30")]
        [Display(Name = "Broj igrača")]
        public int BrojIgraca { get; set; }

        [Required(ErrorMessage = "Stadion je obavezan")]
        [StringLength(100, ErrorMessage = "Naziv stadiona ne može biti duži od 100 karaktera")]
        [Display(Name = "Stadion")]
        public string Stadion { get; set; }

        [Required(ErrorMessage = "Broj osvojenih titula je obavezan")]
        [Range(0, 1000, ErrorMessage = "Broj titula mora biti između 0 i 1000")]
        [Display(Name = "Broj osvojenih titula")]
        public int BrojOsvojenihTitula { get; set; }

        [Required(ErrorMessage = "Morate izabrati turnir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati turnir.")]
        [Display(Name = "TurnirID")]
        public int? TurnirID { get; set; }
        [Display(Name = "Turnir")]
        public string? NazivTurnira { get; set; }
        public ICollection<Turnir>? Turniri { get; set; }
    }
}