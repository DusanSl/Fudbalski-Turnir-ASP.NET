using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnir.DAL.Models
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Datum rođenja")]
        public DateTime DatumRodjenja { get; set; }
        [MaxLength(50)]
        public string Nacionalnost { get; set; }
        public Osoba()
        {

        }
    }
}
