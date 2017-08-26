using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GithubAPI.Models;

namespace GithubAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult About() => View();

        public IActionResult BestProjects()
        {
            List<Repo> listRepo = Repo.GetStarredRepoes();
            return View(listRepo);
        }
    }
}
