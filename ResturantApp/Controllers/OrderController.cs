using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantApp.BL.interfaces;
using RestaurantApp.BL.Services;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Entities;
using System.Linq;

namespace ResturantApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomer customer;
        private readonly IMapper mapper;
        private readonly IWorker worker;
        private readonly IProduct product;
        private readonly IOrder _order;
        private readonly IProductOrder _productorder;

        public OrderController(ICustomer customer,IMapper mapper,IWorker worker,IProduct Product,IOrder order,IProductOrder productorder)
        {
            this.customer = customer;
            this.mapper = mapper;
            this.worker = worker;
            product = Product;
            this._order = order;
            this._productorder = productorder;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(OrderPhoneVM orderPhone) {

            if (ModelState.IsValid)
            {
               var data= customer.GetAll().Where(a=>a.Phone==orderPhone.Phone).ToList();
                if(data.Count()>0)
                {
                    return RedirectToAction("MakeOrder", new {id=data.ElementAt(0).Id});
                }
                else
                {
                    ModelState.AddModelError("", "Not Existed Phone");
                    return View(orderPhone);
                }

            }
            return View(orderPhone);

        }

        public IActionResult MakeOrder(int id)
        {
            var data=customer.GetById(id);
            var result=mapper.Map<CustomerVM>(data);
            ViewBag.Workers = new SelectList(worker.GetAll(), "Id", "Name");
            ViewBag.Products = new SelectList(product.GetAll(), "Id", "Name");
            return View(result);
        }

        [HttpPost]
        public JsonResult Fillselect(string[] arr)
        {
            var indexes = arr.ToList();
            var data = product.GetAll().Where(a => !indexes.Contains(a.Id.ToString()));
            return Json(new { data=data });
        }

        [HttpPost]
      public JsonResult Submit(int[]ids,int [] quantity,int customerid,int waiterid)
        {
            try
            {

        
            List<ProductOrder> productOrders = new List<ProductOrder>();
            Order order = new Order
            {
                customer_id = customerid,
                Date = DateTime.Now,
                workerid = waiterid,

            };
             var orderid = _order.create(order);
                 ;

            for (int i=0;i<ids.Length;i++)
            {
                ProductOrder productOrder = new ProductOrder
                {
                    Order_id = orderid,
                    Product_id = ids[i],
                    Quantity = quantity[i]
                    
                };
                productOrders.Add(productOrder);
            }
            _productorder.CreateRange(productOrders);

            return Json(new {Status=true,orderid=orderid});
            }catch(Exception ex)
            {
                return Json(new { Status = false });
            }
        }

        public IActionResult OrderDetails(int id)
        {
            var data=_order.GetById(id);
            var Result=mapper.Map<OrderVM>(data);
            return View(Result);
        }

        public async Task<IActionResult> sendmail(string message,string customerEmail,int id)
        {
           var state = await MailSender.sendmail(customerEmail, "Your Order Details (ID:"+id+")", message);
            if (state)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return RedirectToAction("OrderDetails", "Order", new {id=id});


            }
            
        }

        public IActionResult ShowData()
        {
            var data = _order.GetAll();
            var result = mapper.Map<IEnumerable<OrderVM>>(data);
            return View(result);
        }

        public IActionResult Delete(int id)
        {
            var data = _order.GetById(id);
            var Result = mapper.Map<OrderVM>(data);
            return View(Result);
        }
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult ConDelete(int id)
        {
            try
            {
                _order.Delete(id);
                return RedirectToAction("ShowData");

            }catch(Exception ex)
            {
                var data = _order.GetById(id);
                var Result = mapper.Map<OrderVM>(data);
                return View(Result);
            }
        }
    }
}
