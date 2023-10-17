using AutoMapper;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.AutoMapper
{
    public class DomainProfile:Profile {

        public DomainProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();
            CreateMap<Customer, CustomerVM>();
            CreateMap<CustomerVM, Customer>();
            CreateMap<Worker, WorkerVM>();
            CreateMap<WorkerVM, Worker>();
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
            //CreateMap<Worker, WorkerVM>();
            //CreateMap<WorkerVM, Worker>();
        }
    }
}
