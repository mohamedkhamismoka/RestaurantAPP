using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.interfaces
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();
        void Create(Customer customer);
        void Update(Customer customer);
        void delete(int id);
        Customer GetById(int id);
    }
}
