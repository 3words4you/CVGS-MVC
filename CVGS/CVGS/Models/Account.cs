using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    public class Account
    {
        [Required]
        [Display(Name = "Username")]
        public string inputUsername { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string inputPassword { get; set; }
    }

   
}