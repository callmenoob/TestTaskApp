using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestTaskApp.DataBase;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ITestBase _testBase;
        private string sqlcmd;

        public CinemasController(ITestBase testBase)
        {
            _testBase = testBase;
        }
        public IActionResult Index()
        {
            sqlcmd = "SELECT * FROM PLACE";
            var result = _testBase.Execute<Cinema>(sqlcmd, new Cinema());
            return View(result.ToList());
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Place where ID = {id}";
            var result = _testBase.Execute<Cinema>(sqlcmd, new Cinema());
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("ID, Name")] Cinema movie)
        {
            sqlcmd = $"Update Place set Name = @Name where ID = {id}";
            _testBase.Execute<Cinema>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ID, Name")] Cinema movie)
        {
            sqlcmd = "Insert into Place Values (@Name)";
            _testBase.Execute<Cinema>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            sqlcmd = $"Delete from Place where ID = {id}";
            _testBase.Execute<Cinema>(sqlcmd, new Cinema());
            return RedirectToAction(nameof(Index));
        }
    }
}
