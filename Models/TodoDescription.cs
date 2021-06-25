using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doit.Models
{
    public class TodoDescription
    {
        [Key]
        public int Id { get; set; }
        public int TodoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
