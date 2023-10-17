
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

        public IEnumerable<AuthenticationScheme>? ExternalLogin { get; set; }


    }
}
