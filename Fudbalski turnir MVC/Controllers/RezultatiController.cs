using FudbalskiTurnir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskiTurnir.Controllers
{
    public class RezultatiController : Controller
    {
        private readonly ITurnirService _turnirService;

        public RezultatiController(ITurnirService turnirService)
        {
            _turnirService = turnirService;
        }

        public async Task<IActionResult> Index()
        {
            var sveUtakmice = await _turnirService.GetAllUtakmiceAsync();

            var sortirano = sveUtakmice.OrderByDescending(u => u.Datum).ToList();

            return View(sortirano);
        }
    }
}