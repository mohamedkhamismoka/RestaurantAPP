using RestaurantApp.BL.interfaces;
using RestaurantApp.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.Reposatory
{
    public class ProductRepo:IProduct
    {
        private readonly DataContext db;

        public ProductRepo(DataContext db)
        {
            this.db = db;
        }
        public void Create(DAL.Entities.Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            db.Products.Remove(db.Products.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<DAL.Entities.Product> GetAll()
        {
            return db.Products.Select(A => A);
        }

        public DAL.Entities.Product GetById(int id)
        {
            return db.Products.Where(a => a.Id == id).FirstOrDefault();
        }

        public void Update(DAL.Entities.Product product)
        {
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
