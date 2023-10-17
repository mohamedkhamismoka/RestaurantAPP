using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.interfaces
{
    public interface IOrder
    {

        public int create(Order order);
        public Order GetById(int id);
        public IEnumerable<Order> GetAll();
        public void Delete(int id);
    }
}
