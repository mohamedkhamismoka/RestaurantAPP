using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

       public string ImgName { get;set; }

        public string Description { get; set; }
        public int unitPrice { get;set; }
        public IEnumerable<ProductOrder> ProductOrders { get; set; }

    }
}
