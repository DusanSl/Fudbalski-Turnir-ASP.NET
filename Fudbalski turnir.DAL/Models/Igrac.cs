using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    [Table("Igraci")]
    public class Igrac : Osoba
    {
        public int KlubID { get; set; }
        [MaxLength(30)]
        public string Pozicija { get; set; }
        public int BrojDresa { get; set; }
        
        public Igrac()
        {

        }
    }
}
