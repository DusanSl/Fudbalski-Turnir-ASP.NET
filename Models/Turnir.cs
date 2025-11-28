namespace Fudbalski_turnir.Models
{
    public class Turnir
    {
       public int TurnirID { get; set; }
       public string NazivTurnira { get; set; }
       public string Lokacija { get; set; }
       public DateTime DatumPocetka { get; set; }
       public DateTime DatumZavrsetka { get; set; }
       public string TipTurnira { get; set; }
    }
}
