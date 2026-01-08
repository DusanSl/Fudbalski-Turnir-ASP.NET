namespace FudbalskiTurnir.BLL.DTOs
{
    public class StandingsDTO
    {
        public int KlubID { get; set; }
        public string KlubNaziv { get; set; } = string.Empty;
        public int Odigrano { get; set; }
        public int Pobede { get; set; }
        public int Nereseno { get; set; }
        public int Porazi { get; set; }
        public int DatiGolovi { get; set; }
        public int PrimljeniGolovi { get; set; }
        public int GolRazlika => DatiGolovi - PrimljeniGolovi;
        public int Bodovi { get; set; }
    }

    public class TurnirPregledDTO
    {
        public Dictionary<string, List<StandingsDTO>> Grupe { get; set; } = new();
        public BracketDTO Bracket { get; set; } = new();
    }

    public class BracketDTO
    {
        public List<KnockoutUtakmicaDTO> OsminaFinala { get; set; } = new();
        public List<KnockoutUtakmicaDTO> Cetvrtfinale { get; set; } = new();
        public List<KnockoutUtakmicaDTO> Polufinale { get; set; } = new();
        public KnockoutUtakmicaDTO? Finale { get; set; }
    }

    public class KnockoutUtakmicaDTO
    {
        public int UtakmicaID { get; set; }
        public string PrviKlubNaziv { get; set; } = string.Empty;
        public string DrugiKlubNaziv { get; set; } = string.Empty;
        public int? PrviKlubGolovi { get; set; }
        public int? DrugiKlubGolovi { get; set; }
        public int? PrviKlubPenali { get; set; }
        public int? DrugiKlubPenali { get; set; }
        public string Pobednik { get; set; } = string.Empty; 
        public bool Produzeci { get; set; }
    }
}