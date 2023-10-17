using Microsoft.AspNetCore.Http;
using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.VM
{
    public class ProductVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="Product Name is required")]
        [MinLength(3,ErrorMessage ="MinLength is 3 characters")]
        public string Name { get; set; }


        public string? ImgName { get; set; }
        [Required(ErrorMessage = "Product Image is required")]
        public IFormFile Img {  get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [MinLength(10, ErrorMessage = "MinLength is 10 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(1,100000,ErrorMessage = "Unit Price must be between 1 and 100000")]
        public int unitPrice { get; set; }
        public IEnumerable<ProductOrder>? ProductOrders { get; set; }
    }
}
