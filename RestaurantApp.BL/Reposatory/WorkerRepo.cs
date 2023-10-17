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
    public class WorkerRepo : IWorker
    {
        private readonly DataContext db;

        public WorkerRepo(DataContext db)
        {
            this.db = db;
        }
        public void Create(Worker worker)
        {
            db.Workers.Add(worker);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            db.Workers.Remove(db.Workers.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Worker> GetAll()
        {
            return db.Workers.Select(a => a);
        }

        public Worker GetById(int id)
        {
            return db.Workers.Find(id);
        }

        public void Update(Worker worker)
        {
            db.Entry(worker).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
