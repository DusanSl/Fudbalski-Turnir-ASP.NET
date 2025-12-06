using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    [Table("Menadzeri")]
    public class Menadzer : Osoba
    {
        public int KlubID { get; set; }
        public int GodineIskustva { get; set; }

        public Menadzer()
        {

        }
    }
}
