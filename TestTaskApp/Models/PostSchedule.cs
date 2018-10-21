using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApp.Models
{
    public class PostSchedule
    {
        public int MovieID { get; set; }
        public int CinemaID { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string SessionTime { get; set; }
    }
}
