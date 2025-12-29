namespace Fudbalski_turnir.ViewModels
{
    public class KnockoutViewModel
    {
        public string Faza { get; set; } 
        public List<KnockoutUtakmica> Utakmice { get; set; }
    }

    public class KnockoutUtakmica
    {
        public int UtakmicaID { get; set; }
        public string PrviKlubNaziv { get; set; }
        public string DrugiKlubNaziv { get; set; }
        public int PrviKlubGolovi { get; set; }
        public int DrugiKlubGolovi { get; set; }
        public bool? Produzeci { get; set; }
        public bool? Penali { get; set; }
        public int? PrviKlubPenali { get; set; }
        public int? DrugiKlubPenali { get; set; }
        public string Pobednik { get; set; }
        public DateTime? Datum { get; set; }
        public string Mesto { get; set; }
    }

    public class BracketViewModel
    {
        public List<KnockoutUtakmica> OsminaFinala { get; set; }
        public List<KnockoutUtakmica> Cetvrtfinale { get; set; }
        public List<KnockoutUtakmica> Polufinale { get; set; }
        public KnockoutUtakmica Finale { get; set; }
    }
}
