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
    public class OrderRepo : IOrder
    {
        private readonly DataContext db;

        public OrderRepo(DataContext db)
        {
            this.db = db;
        }
        public int create(Order order)
        {
            var data = db.orders.Add(order);
            db.SaveChanges();
            return data.Entity.Id;
        }

        public Order GetById(int id)
        {
            return db.orders.Find(id);
        }
        public IEnumerable<Order> GetAll()
        {
            return db.orders.Select(a => a).Include("customer").Include("worker");
        }

        public void Delete(int id) {
            db.Remove(db.orders.Find(id));
            db.SaveChanges();
        }
    }
}
