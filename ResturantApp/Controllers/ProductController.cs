using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.BL.interfaces;
using RestaurantApp.BL.Services;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Entities;

namespace ResturantApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _IProduct;
        private readonly IMapper _mapper;

        public ProductController(IProduct IProduct,IMapper mapper)
        {
          this. _IProduct = IProduct;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = _IProduct.GetAll();
            var result=_mapper.Map<IEnumerable<ProductVM>>(data);  

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(ProductVM product)
        {
            try
            {
             
                    if (ModelState.IsValid)
                    {
                        var imgname = FileUploader.upload("productImages", product.Img);
                        product.ImgName = imgname;
                        var data = _mapper.Map<Product>(product);
                        _IProduct.Create(data);
                        return RedirectToAction("Index");

                    }
                    return View(product);
                
               
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(product);
            }
        }

        public IActionResult Details(int id)
        {
            var data = _IProduct.GetById(id);
            var result = _mapper.Map<ProductVM>(data);
            return View(result);
        }

        public IActionResult Update(int id)
        {
            var data = _IProduct.GetById(id);
           

            var result = _mapper.Map<ProductVM>(data);
          
            return View(result);
        }
        [HttpPost]
        public IActionResult Update( ProductVM product,string oldimgname)
        {
            try
            {
                var imgname = "";
                ModelState.Root.Children[1].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

                if (ModelState.IsValid)
                {

                    if (product.Img != null)
                    {
                        FileUploader.delete("productImages", oldimgname);
                         imgname = FileUploader.upload("productImages", product.Img);
                    }
                    else
                    {
                        imgname = oldimgname;
                    }
                  
                    var data = _mapper.Map<Product>(product);
                   
                  
                    data.ImgName=imgname;
                    _IProduct.Update(data);
                    return RedirectToAction("Index","Product");
                }
                return View(product);
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(product);
            }
          
        }
        public IActionResult Delete(int id)
        {
            var data = _IProduct.GetById(id);
            var result = _mapper.Map<ProductVM>(data);
            return View(result);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                _IProduct.delete(id);
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                ViewBag.error=e.Message;
                return View();
            }
        }
    }
}
