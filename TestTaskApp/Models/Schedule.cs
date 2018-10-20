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
        public Cinema Cinemas { get; set; }
        [Required]
        public Movie Movies { get; set; }
        [Required]
        public DateTime ScheduleDate { get; set;}
        [Required]
        public List<Session> Sessions { get; set; }
    }
}
