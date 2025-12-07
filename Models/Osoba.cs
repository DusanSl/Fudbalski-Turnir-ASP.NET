using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fudbalski_turnir.Models
{
    public class Osoba
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OsobaID { get; set; }
        [MaxLength(50)]
        public string Ime { get; set; }
        [MaxLength(100)]
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        [MaxLength(50)]
        public string Nacionalnost { get; set; }
        public DateTime UKlubuOd { get; set; } // klub u kojem osoba trenutno igra ili radi
        public DateTime UKlubuDo { get; set; }

        public Osoba()
        {

        }
    }
}
