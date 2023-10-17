using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.interfaces
{
    public interface IWorker
    {
        IEnumerable<Worker> GetAll();
        void Create(Worker worker);
        void Update(Worker worker);
        void delete(int id);
        Worker GetById(int id);
    }
}
