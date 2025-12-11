using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    [Table("Menadzeri")]
    public class Menadzer : Osoba
    {
        public int KlubID { get; set; }
        [Display(Name = "Godine iskustva")]
        public int GodineIskustva { get; set; }

        public Menadzer()
        {

        }
    }
}
