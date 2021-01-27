using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Controllers
{
    public class AptidoesCompetenciasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
