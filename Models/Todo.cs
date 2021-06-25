using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Doit.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        [NotMapped]
        public TodoDescription TodoDescription { get; set; }
        [NotMapped]
        public List<TodoDescription> TodoDescriptions { get; set; }
    }
}
