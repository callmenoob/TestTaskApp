using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApp.Models
{
    public class Schedule
    {
        [Required]
        public Cinema Cinema { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public DateTime ScheduleDate { get; set;}
        [Required]
        public List<Session> Sessions { get; set; }
    }
}
