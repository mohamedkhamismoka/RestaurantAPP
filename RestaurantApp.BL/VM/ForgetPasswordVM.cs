using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class ForgetPasswordVM
    {
        
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }
    }
}
