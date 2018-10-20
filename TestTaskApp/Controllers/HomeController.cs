using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestTaskApp.DataBase;
using TestTaskApp.DataBase.Commands;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{
    public class HomeController : Controller
    {
        private ITestBase _testBase;
        public HomeController(ITestBase testBase)
        {
            _testBase = testBase;
            string sql = "INSERT INTO MOVIE VALUES (@NAME)";
            var insertcmd = new Insert(_testBase);
            insertcmd.Execute<Movie>(sql, new Movie());

        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
