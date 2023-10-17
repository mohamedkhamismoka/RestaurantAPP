using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.BL.interfaces;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Entities;

namespace ResturantApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _icustomer;
        private readonly IMapper mapper;

        public CustomerController(ICustomer Icustomer,IMapper mapper)
        {
            _icustomer = Icustomer;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data=_icustomer.GetAll();
            var result = mapper.Map<IEnumerable<CustomerVM>>(data);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Customer>(customer);
                    _icustomer.Create(data);
                    return RedirectToAction("Index");
                }
                return View(customer);
            }catch(Exception ex)
            {
                return View(customer);
            }
            
        }

        public IActionResult Details(int id)
        {
            var data = _icustomer.GetById(id);
            var result = mapper.Map<CustomerVM>(data);
            return View(result);
        }
        public IActionResult Delete(int id)
        {
            var data = _icustomer.GetById(id);
            var result=mapper.Map<CustomerVM>(data);
            return View(result);
        }

        public IActionResult Update(int id)
        {
            var data = _icustomer.GetById(id);
            var result = mapper.Map<CustomerVM>(data);
            return View(result);
        }

        [HttpPost]
        public IActionResult Update(CustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {var data = mapper.Map<Customer>(customer);
                    _icustomer.Update(data);
                    return RedirectToAction("Index");
                }
                return View(customer);
            }catch(Exception ex)
            {
                return View(customer);
            }
           
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                _icustomer.delete(id);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                var data = _icustomer.GetById(id);
                var result = mapper.Map<CustomerVM>(data);
                return View(result);
             
            }
        }
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var data=_icustomer.GetAll().Where(a=>a.Email.StartsWith(email));
            if (data.Count()==0)
            {
                return Json(new { result = false });
            }
            else
            {
                return Json(new { result = true });
            }
        }

        [HttpPost]
        public JsonResult CheckPhone(string Phone)
        {
            var data = _icustomer.GetAll().Where(a => a.Phone.StartsWith(Phone));
            if (data.Count() == 0)
            {
                return Json(new { result = false });
            }
            else
            {
                return Json(new { result = true });
            }
        }

        [HttpPost]
        public JsonResult CheckEmailForUpdate(string email, string oldemail)
        {
            var data = _icustomer.GetAll().Where(a => a.Email.StartsWith(email)&&a.Email!=oldemail);
            if (data.Count() == 0)
            {
                return Json(new { result = false });
            }
            else
            {
                return Json(new { result = true });
            }
        }

        [HttpPost]
        public JsonResult CheckPhoneForUpdate(string Phone,string oldphone)
        {
            var data = _icustomer.GetAll().Where(a => a.Phone.StartsWith(Phone)&&a.Phone!=oldphone);
            if (data.Count() == 0)
            {
                return Json(new { result = false });
            }
            else
            {
                return Json(new { result = true });
            }
        }
    }
}
