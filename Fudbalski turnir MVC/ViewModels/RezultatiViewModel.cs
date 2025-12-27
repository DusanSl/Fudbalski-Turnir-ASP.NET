using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.ViewModels
{
    public class RezultatiViewModel
    {
        public List<Utakmica> Utakmice { get; set; }
        public string NaslovStranice { get; set; }
        public int UkupnoUtakmica => Utakmice.Count;
        public int? IzabraniTurnirID { get; set; }
    }
}