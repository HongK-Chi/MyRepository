using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class TESTController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
