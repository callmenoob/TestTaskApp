using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskApp.DataBase;
using TestTaskApp.DataBase.Commands;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ITestBase _testBase;
        private string sqlcmd;
        public ScheduleController(ITestBase testBase)
        {
            _testBase = testBase;
        }

        public IActionResult Index()
        {
            sqlcmd = "SELECT * FROM Schedule";
            var insertcmd = new Insert(_testBase);
            var result = insertcmd.Execute<Schedule>(sqlcmd, new Schedule());
            return View(result);
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Schedule where SessionID = {id}";
            var cmd = new Insert(_testBase);
            var result = cmd.Execute<Schedule>(sqlcmd, new Schedule());
            sqlcmd = "SELECT * FROM MOVIE ";
            var insertcmd = new Insert(_testBase);
            var Movies = insertcmd.Execute<Movie>(sqlcmd, new Movie());
            sqlcmd = "SELECT * FROM Place ";

            var Cinemas = insertcmd.Execute<Cinema>(sqlcmd, new Cinema());

            ViewBag.Movies = new SelectList(Movies, "ID", "Name", result.FirstOrDefault().MovieName);
            ViewBag.Cinemas = new SelectList(Cinemas, "ID", "Name", result.FirstOrDefault().CinemaName);

            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("MovieName, CinemaName, SessionTime")] Schedule schedule)
        {
            PostSchedule post = new PostSchedule()
            {
                MovieID = Convert.ToInt32(schedule.MovieName),
                CinemaID = Convert.ToInt32(schedule.CinemaName),
                ScheduleDate = schedule.SessionTime
            };
        
            sqlcmd = $"Update Session set MovieID = @MovieID," +
                $" CinemaID = @CinemaID," +
                $" SessionDate = @ScheduleDate where SessionID = {id}";
            var cmd = new Insert(_testBase);
            cmd.Execute<PostSchedule>(sqlcmd, post);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Post()
        {
            sqlcmd = "SELECT * FROM MOVIE ";
            var insertcmd = new Insert(_testBase);
            var Movies = insertcmd.Execute<Movie>(sqlcmd, new Movie());
            sqlcmd = "SELECT * FROM Place ";

            var Cinemas = insertcmd.Execute<Cinema>(sqlcmd, new Cinema());

            ViewBag.Movies = new SelectList(Movies, "ID", "Name");
            ViewBag.Cinemas = new SelectList(Cinemas, "ID", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Post([Bind("MovieID, CinemaID, ScheduleDate, SessionTime")] PostSchedule schedule)
        {
            PostSchedule schedule1 = schedule;
            var time = schedule1.SessionTime.Split(' ');
            var dateTime = time.Select(x => DateTime.Parse(x));

            List<PostSchedule> indatabase = new List<PostSchedule>();
            foreach(var date in dateTime)
            {
                indatabase.Add(new PostSchedule()
                {
                    CinemaID = schedule1.CinemaID,
                    MovieID = schedule1.MovieID,
                    ScheduleDate = schedule1.ScheduleDate.AddHours(date.Hour).AddMinutes(date.Minute)
                });
            }

            sqlcmd = "INSERT INTO Session VALUES (@MovieID, @CinemaID, @ScheduleDate) ";
            var insertcmd = new Insert(_testBase);
            foreach (var item in indatabase)
            {
                insertcmd.Execute<PostSchedule>(sqlcmd, item);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            sqlcmd = $"Delete from Session where SessionID = {id}";
            var cmd = new Insert(_testBase);
            cmd.Execute<Schedule>(sqlcmd, new Schedule());
            return RedirectToAction(nameof(Index));
        }

    }
}
