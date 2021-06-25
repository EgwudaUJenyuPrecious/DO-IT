using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Doit.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Fullname { get; set; }
       
        public string Telephone { get; set; }
        
        

    }
}
