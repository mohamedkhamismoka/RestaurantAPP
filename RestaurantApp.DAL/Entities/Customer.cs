using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Entities
{
    public class Customer
    {
   
        public int Id { get; set; }
        [StringLength(20)]

        public string Name { get; set; }
        public string Address { get; set; }
       
        public string Phone { get; set; }
        
        public string Email { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
