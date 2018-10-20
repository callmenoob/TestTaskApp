using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskApp.DataBase;
using TestTaskApp.DataBase.Commands;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{
    public class MoviesController : Controller
    {

        private ITestBase _testBase;
        private string sqlcmd;
        public MoviesController(ITestBase testBase)
        {
            _testBase = testBase;
        }

        public IActionResult Index()
        {
            sqlcmd = "SELECT * FROM MOVIE ";
            var insertcmd = new Insert(_testBase);
            Movie list = null;
            var result = insertcmd.Execute<Movie>(sqlcmd, list);
            return View(result.ToList());
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Movie where ID = {id}";
            var cmd = new Insert(_testBase);
            var result = cmd.Execute<Movie>(sqlcmd, new Movie());
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("ID, Name")] Movie movie )
        {
            sqlcmd = $"Update Movie set Name = @Name where ID = {id}";
            var cmd = new Insert(_testBase);
            cmd.Execute<Movie>(sqlcmd, movie);
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
            var cmd = new Insert(_testBase);
            cmd.Execute<Movie>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([Bind("ID")]Movie movie)
        {
            sqlcmd = "Delete from Movie where ID = @ID";
            var cmd = new Insert(_testBase);
            cmd.Execute<Movie>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
