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
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Pozicija { get; set; } = string.Empty;
        public int BrojDresa { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Nacionalnost { get; set; } = null!;
        public int KlubID { get; set; }
        public string? ImeKluba { get; set; }
    }
}
