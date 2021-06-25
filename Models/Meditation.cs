using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Doit.Models
{
    public class Meditation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string AudioUrl { get; set; }

        [NotMapped]
        public IFormFile VideoFile { get; set; } 
        [NotMapped]
        public IFormFile AudioFile { get; set; } 
            


    }
}
