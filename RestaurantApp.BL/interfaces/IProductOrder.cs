using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.interfaces
{
    public interface IProductOrder
    {
        public void CreateRange(IEnumerable<ProductOrder> productOrders);
        public IEnumerable<ProductOrder> getbyOrderID(int id);
        public int getPrice(int orderid);
    }
}
