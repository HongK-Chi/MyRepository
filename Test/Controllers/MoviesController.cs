using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Movie()
        {
            var movie = new Movie() { Name = "Test" };

            //return View(movie);
            //return new ViewResult();
            //return Content("Hello World!");
            //return new EmptyResult();
            return RedirectToAction("Privacy", "Home", new { page = 1, sortBy = "name" });
        }

        public ActionResult Edit (int movieId)
        {
            return Content("id= " + movieId);
        }

        public ActionResult Index (int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }
    }
}
