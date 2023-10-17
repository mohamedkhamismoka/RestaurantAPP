using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class CustomerVM
    {
       
        public int Id { get; set; }
        [MinLength(10,ErrorMessage ="min length is 10 characters")]
        [Required( ErrorMessage = "customer name is required")]

        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "min length is 10 characters")]
        [Required(ErrorMessage = "customer address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "customer Phone is required")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "customer Email is required")]
        public string Email { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
