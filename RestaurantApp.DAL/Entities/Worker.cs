﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string imgname { get; set; }
        [Column(TypeName ="Date")]
        public DateTime BirthDate {  get; set; }
        public string CVname { get; set; }

        [Column(TypeName = "Date")]
        public DateTime HireDate {  get; set; }
        public int Salary { get; set; }
        public IEnumerable<Order> orders { get; set; }

    }
}
