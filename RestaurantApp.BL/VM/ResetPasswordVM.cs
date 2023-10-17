using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Compare("Password")]
        [Required(ErrorMessage ="Confirm Password is Required")]
        public string confirmPassword { get; set; }

        public string email { get; set; }
        public string token { get; set; }
    }
}
