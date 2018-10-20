using System.ComponentModel.DataAnnotations;

namespace TestTaskApp.Models
{
    public class Cinema
    {        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
