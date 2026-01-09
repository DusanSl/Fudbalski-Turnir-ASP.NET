using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.ViewModels
{
    public class RezultatiViewModel
    {
        public List<Utakmica> Utakmice { get; set; } = new();
        public string NaslovStranice { get; set; } = string.Empty;
        public int UkupnoUtakmica => Utakmice.Count;
        public int? IzabraniTurnirID { get; set; }
    }
}