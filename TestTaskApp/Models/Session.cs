using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApp.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public Movie Movie { get; set; }
        public Cinema Cinema {get; set; }
        public DateTime DateTime { get; set; }
    }
}
