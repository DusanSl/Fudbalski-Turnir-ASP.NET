using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    [Table("Igraci")]
    public class Igrac : Osoba
    {
        [ForeignKey("KlubID")]
        public int KlubID { get; set; }
        public Klub? Klub { get; set; }
        [Required]
        [MaxLength(30)]
        public string Pozicija { get; set; } = string.Empty;
        [Required]
        public int BrojDresa { get; set; }
        public Igrac()
        {

        }
    }
}