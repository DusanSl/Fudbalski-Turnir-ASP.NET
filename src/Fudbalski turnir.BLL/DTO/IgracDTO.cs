using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fudbalski_turnir.BLL.DTO
{
    public class IgracDTO
    {
        public int OsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
        public int BrojDresa { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Nacionalnost { get; set; }
        public int KlubID { get; set; }
        public string? ImeKluba { get; set; }
    }
}
