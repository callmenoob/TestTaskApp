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
    public class CinemasController : Controller
    {
        private ITestBase _testBase;
        private string sqlcmd;

        public CinemasController(ITestBase testBase)
        {
            _testBase = testBase;
        }
        public IActionResult Index()
        {
            sqlcmd = "SELECT * FROM PLACE";
            var insertcmd = new Insert(_testBase);
            Cinema list = null;
            var result = insertcmd.Execute<Cinema>(sqlcmd, list);
            return View(result.ToList());
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Place where ID = {id}";
            var cmd = new Insert(_testBase);
            var result = cmd.Execute<Cinema>(sqlcmd, new Cinema());
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("ID, Name")] Cinema movie)
        {
            sqlcmd = $"Update Place set Name = @Name where ID = {id}";
            var cmd = new Insert(_testBase);
            cmd.Execute<Cinema>(sqlcmd, movie);
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
            var cmd = new Insert(_testBase);
            cmd.Execute<Cinema>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([Bind("ID")]Cinema movie)
        {
            sqlcmd = "Delete from Place where ID = @ID";
            var cmd = new Insert(_testBase);
            cmd.Execute<Cinema>(sqlcmd, movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
