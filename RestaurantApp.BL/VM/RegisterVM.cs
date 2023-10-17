using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="username is required")]
        [MinLength(10,ErrorMessage ="MinLength is 10 Characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password")]
        [Required(ErrorMessage ="Confirm Password is required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }


       

    }
}
