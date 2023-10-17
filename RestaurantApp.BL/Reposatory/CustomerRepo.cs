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
    public class CustomerRepo : ICustomer

    {
        private readonly DataContext db;

      public CustomerRepo( DataContext db)
        {
            this.db = db;
        }
        public void Create(Customer customer)
        {
            db.customers.Add(customer);
            db.SaveChanges();
        }

        public void delete(int id)
        {
         db.customers.Remove(db.customers.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.customers.Select(a => a);
        }

        public Customer GetById(int id)
        {
            return db.customers.Where(a=>a.Id==id).FirstOrDefault();
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
