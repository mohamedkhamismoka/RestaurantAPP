using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Entities
{
    public  class Order
    {
    
        public int Id { get; set; }
        [Column(TypeName = "Date")]

        public DateTime Date { get; set; }

        public int  customer_id { get; set; }
        [ForeignKey("customer_id")]
        public Customer  customer { get; set; }

        public IEnumerable<ProductOrder> ?ProductOrders { get; set; }
        [ForeignKey("workerid")]
        public Worker worker { get; set; }
        public int  workerid { get; set; }
    }
}
