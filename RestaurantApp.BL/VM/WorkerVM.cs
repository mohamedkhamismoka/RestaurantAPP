using Microsoft.AspNetCore.Http;
using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public  class WorkerVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Worker Name is Required")]
        [MinLength(10,ErrorMessage = "min Length is 10 chararacters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Worker address is Required")]
        [MinLength(10, ErrorMessage = "min Length is 10 chararacters")]
        public string Address { get; set; }
        public string? imgname { get; set; }
        [Required(ErrorMessage = "Worker Birthdate is Required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? CVname { get; set; }
        [Required(ErrorMessage = "Worker hiredate is Required")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Worker salary is Required")]
        [Range(1000,50000,ErrorMessage ="Salary must be between 1000 and 50000")]
        public int Salary { get; set; }
        public IEnumerable<Order>? orders { get; set; }
        [Required(ErrorMessage = "Worker cv is Required")]
        public IFormFile CV {  get; set; }
        [Required(ErrorMessage = "Worker image is Required")]
        public IFormFile Img { get; set; }

    }
}
