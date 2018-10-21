using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskApp.DataBase;
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
            var result = _testBase.Execute<Schedule>(sqlcmd, new Schedule());
            return View(result);
        }

        public IActionResult Edit(int id)
        {
            sqlcmd = $"Select * from Schedule where SessionID = {id}";
            var result = _testBase.Execute<Schedule>(sqlcmd, new Schedule());

            sqlcmd = "SELECT * FROM MOVIE ";
            var Movies = _testBase.Execute<Movie>(sqlcmd, new Movie());

            sqlcmd = "SELECT * FROM Place ";
            var Cinemas = _testBase.Execute<Cinema>(sqlcmd, new Cinema());

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

            _testBase.Execute<PostSchedule>(sqlcmd, post);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Post()
        {
            sqlcmd = "SELECT * FROM MOVIE ";
            var Movies = _testBase.Execute<Movie>(sqlcmd, new Movie());

            sqlcmd = "SELECT * FROM Place ";
            var Cinemas = _testBase.Execute<Cinema>(sqlcmd, new Cinema());

            ViewBag.Movies = new SelectList(Movies, "ID", "Name");
            ViewBag.Cinemas = new SelectList(Cinemas, "ID", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Post([Bind("MovieID, CinemaID, ScheduleDate, SessionTime")] PostSchedule schedule)
        {
            PostSchedule schedule1 = schedule;
            List<PostSchedule> indatabase = new List<PostSchedule>();
            sqlcmd = "INSERT INTO Session VALUES (@MovieID, @CinemaID, @ScheduleDate)";

            var time = schedule1.SessionTime.Split(' ');
            var dateTime = time.Select(x => DateTime.Parse(x));
            
            foreach(var date in dateTime)
            {
                indatabase.Add(new PostSchedule()
                {
                    CinemaID = schedule1.CinemaID,
                    MovieID = schedule1.MovieID,
                    ScheduleDate = schedule1.ScheduleDate.AddHours(date.Hour).AddMinutes(date.Minute)
                });
            }
        
            foreach (var item in indatabase)
            {
                _testBase.Execute<PostSchedule>(sqlcmd, item);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            sqlcmd = $"Delete from Session where SessionID = {id}";
            _testBase.Execute<Schedule>(sqlcmd, new Schedule());
            return RedirectToAction(nameof(Index));
        }

    }
}
