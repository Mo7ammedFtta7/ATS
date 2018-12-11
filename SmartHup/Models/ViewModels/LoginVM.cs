using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartHup.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

    }
}