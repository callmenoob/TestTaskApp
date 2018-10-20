using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApp.Models
{
    public class Schedule
    {
        public int SessionID { get; set; }
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
        
    }
}
