using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class OrderPhoneVM
    {
        [Required(ErrorMessage ="Phone is Reuired")]
        public string Phone { get; set; }
    }
}
