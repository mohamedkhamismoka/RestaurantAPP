using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Entities
{
    public class ProductOrder
    {
        public int Product_id { get; set; }
        public int Order_id { get; set; }

        [ForeignKey("Product_id")]
        public Product Product { get; set; }

          [ForeignKey("Order_id")]
        public Order Order { get; set; }

        public int Quantity { get; set; }

    }
}
