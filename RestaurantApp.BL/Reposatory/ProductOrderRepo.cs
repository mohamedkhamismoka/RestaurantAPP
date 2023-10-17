using Microsoft.EntityFrameworkCore;
using RestaurantApp.BL.interfaces;
using RestaurantApp.DAL.Database;
using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.Reposatory
{
    public class ProductOrderRepo : IProductOrder
    {
        private readonly DataContext db;
       

        public ProductOrderRepo(DataContext db)
        {
            this.db = db;
          
        }
        public void CreateRange(IEnumerable<ProductOrder> productOrders)
        {
          this.db.ProductOrders.AddRange(productOrders);
            db.SaveChanges();
        }

        public IEnumerable<ProductOrder> getbyOrderID(int id)
        {
            return db.ProductOrders.Where(a => a.Order_id == id).Include("Product");
        }

        public int getPrice(int orderid)
        { int total = 0;
            var data = db.ProductOrders.Where(a => a.Order_id == orderid).Include("Product").AsEnumerable();
           
            for (int i = 0; i < data.Count(); i++)
            {
                total += data.ElementAt(i).Quantity *data.ElementAt(i).Product.unitPrice;
            }
            return total;
        }
    }
}
