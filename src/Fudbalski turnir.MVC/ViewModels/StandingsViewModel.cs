public class StandingsViewModel
{
    public int KlubID { get; set; } 
    public string KlubNaziv { get; set; }
    public int Odigrano { get; set; }
    public int Pobede { get; set; }
    public int Nereseno { get; set; }
    public int Porazi { get; set; }
    public int DatiGolovi { get; set; }
    public int PrimljeniGolovi { get; set; }
    public int GolRazlika => DatiGolovi - PrimljeniGolovi;
    public int Bodovi { get; set; }
}
public class GrupnaFazaViewModel
{
    public Dictionary<string, List<StandingsViewModel>> Grupe { get; set; }
    public BracketViewModel Bracket { get; set; }
}
public class BracketViewModel
{
    public List<KnockoutUtakmica> OsminaFinala { get; set; } = new();
    public List<KnockoutUtakmica> Cetvrtfinale { get; set; } = new();
    public List<KnockoutUtakmica> Polufinale { get; set; } = new();
    public KnockoutUtakmica Finale { get; set; } = new();
}

public class KnockoutUtakmica
{
    public int UtakmicaID { get; set; }
    public string PrviKlubNaziv { get; set; } = string.Empty;
    public string DrugiKlubNaziv { get; set; } = string.Empty;
    public int? PrviKlubGolovi { get; set; }
    public int? DrugiKlubGolovi { get; set; }
    public bool Produzeci { get; set; }
    public bool Penali { get; set; }
    public int? PrviKlubPenali { get; set; }
    public int? DrugiKlubPenali { get; set; }
    public string Pobednik { get; set; } = string.Empty;
    public DateTime? Datum { get; set; }
    public string Mesto { get; set; } = string.Empty;
}