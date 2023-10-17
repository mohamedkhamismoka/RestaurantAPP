using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();
        void Create(Product product);
        void Update(Product product);
        void delete(int id);
        Product GetById(int id);
    }
}
