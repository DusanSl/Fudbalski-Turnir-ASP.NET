public class StandingsViewModel
{
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