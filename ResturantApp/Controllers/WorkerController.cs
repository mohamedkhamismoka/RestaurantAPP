using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NuGet.LibraryModel;
using RestaurantApp.BL.interfaces;
using RestaurantApp.BL.Services;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Entities;
using System.Xml.Linq;

namespace ResturantApp.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorker _worker;
        private readonly IMapper mapper;

        public WorkerController(IWorker worker,IMapper mapper)
        {
            this._worker = worker;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = _worker.GetAll();
            var result = mapper.Map<IEnumerable<WorkerVM>>(data);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(WorkerVM worker)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var imgname = FileUploader.upload("Workerimages", worker.Img);
                    var cvname= FileUploader.upload("workercv", worker.CV);
                    worker.CVname = cvname;
                    worker.imgname = imgname;
                    var data = mapper.Map<Worker>(worker);
                    _worker.Create(data);
                    return RedirectToAction("Index");

                }
                return View(worker);
            }
            catch(Exception ex)
            {
                return View(worker);
            }
          
        }

        public IActionResult Details(int id)
        {
            var data = _worker.GetById(id);
            var result = mapper.Map<WorkerVM>(data);
            return View(result);
        }

        public IActionResult Update(int id)
        {
            var data = _worker.GetById(id);
            var result = mapper.Map<WorkerVM>(data);
            return View(result);
        }
        [HttpPost]
        
        public IActionResult Update(WorkerVM worker, string oldimg, string oldcv)
        {

            try
            {

                ModelState.Root.Children[0].ValidationState = ModelValidationState.Valid;
                ModelState.Root.Children[2].ValidationState = ModelValidationState.Valid;

                if (ModelState.IsValid)
                {
                    var cvname = "";
                        var imgname = "";
                    if (worker.CV != null)
                    {
                        FileUploader.delete("workercv", oldcv);
                         cvname = FileUploader.upload("workercv", worker.CV);
                    }
                    else
                    {
                        cvname = oldcv;
                    }

                    if (worker.Img != null)
                    {
                        FileUploader.delete("Workerimages", oldimg);
                         imgname = FileUploader.upload("Workerimages", worker.Img);
                    }
                    else
                    {
                        imgname=oldimg;
                    }
                    worker.imgname = imgname;
                    worker.CVname = cvname;
                    var data = mapper.Map<Worker>(worker);
                    _worker.Update(data);
                    return RedirectToAction("Index");
                }
                return View(worker);
            }catch(Exception ex) { return View(worker);}


        }

        public IActionResult Delete(int id)
        {
            var data = _worker.GetById(id);
            var result = mapper.Map<WorkerVM>(data);
            return View(result);
        }
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult confirmDelete(int id)
        {
            try
            {
             
                _worker.delete(id);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                var data = _worker.GetById(id);
                var result = mapper.Map<WorkerVM>(data);
                return View(result);
            }
           
        }
    }
}
