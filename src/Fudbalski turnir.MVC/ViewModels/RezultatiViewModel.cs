using FudbalskiTurnir.BLL.DTOs;

namespace FudbalskiTurnir.ViewModels
{
    public class RezultatiViewModel
    {
        public ICollection<UtakmicaDTO>? Utakmica { get; set; }
        public string NaslovStranice { get; set; } = string.Empty;
        public int UkupnoUtakmica => Utakmica?.Count ?? 0;
        public int? IzabraniTurnirID { get; set; }
    }
}