using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
{
    [Table("Menadzeri")]
    public class Menadzer : Osoba
    {
        [ForeignKey("KlubID")]
        public int KlubID { get; set; }
        public Klub? Klub { get; set; }
        [Required]
        public int GodineIskustva { get; set; }

        public Menadzer() 
        { 
        
        }
    }
}