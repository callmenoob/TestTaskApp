using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestTaskApp.DataBase;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{
    public class MoviesController : Controller
    {

        private readonly ITestBase _testBase;
        private string sqlcmd;
        public MoviesController(ITestBase testBase)
        {
            _testBase = testBase;
        }

        public IActionResult Index()
        {
            sqlcmd = "SELECT * FROM MOVIE ";
            var result = _testBase.Execute<Movie>(sqlcmd, new Movie());
            return View(result.ToList());
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Movie where ID = {id}";
            var result = _testBase.Execute<Movie>(sqlcmd, new Movie());
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("ID, Name")] Movie movie )
        {
            sqlcmd = $"Update Movie set Name = @Name where ID = {id}";
            _testBase.Execute<Movie>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ID, Name")] Movie movie)
        {
            sqlcmd = "Insert into Movie Values (@Name)";
            _testBase.Execute<Movie>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            sqlcmd = $"Delete from Movie where ID = {id}";
            _testBase.Execute<Movie>(sqlcmd, new Movie());
            return RedirectToAction(nameof(Index));
        }

    }
}
