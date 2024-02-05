using Aj.DataAccess.Repository.IRepository;
using AJ.DataAcess.Data;
using AJ.Models;
using AJ.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace AJStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //In tradition .net application, We had to create Object of ApplicationDb Context
        //.net core provides object of db context as we have regitered it in Program.cs
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;

        //IWebHostEnvironment is used to access wwwroot folder
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository db, ICategoryRepository ct, IWebHostEnvironment webHostEnvironment)
        {
            _product = db;
            _category = ct;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {

            //Using View bag to pass categoryList to View
            //ViewBag.CategoryList = CategoryList;
            //Using ViewData
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _product.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            //Custom Validations
            //if(!obj.Name.IsNullOrEmpty() && !obj.DisplayOrder.ToString().IsNullOrEmpty() && obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name and Display order cannot be exactly same");
            //}
            if (ModelState.IsValid)
            {
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string produtPath = Path.Combine(wwwrootPath, @"images\product");

                    if(!string.IsNullOrEmpty( obj.Product.ImageUrl))
                    {
                        //delete old img
                        string oldFileName = _webHostEnvironment.WebRootPath + obj.Product.ImageUrl.TrimStart('\\');

                        if(System.IO.File.Exists(oldFileName))
                        {
                            System.IO.File.Delete(oldFileName);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(produtPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if(obj.Product.Id == 0) {
                    _product.Add(obj.Product);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    _product.Update(obj.Product);
                    TempData["success"] = "Product Updated Successfully";

                }
                
                _product.Save();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                obj.CategoryList = _category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Product? product = _product.Get(c => c.Id == id); //also work with other fields 
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null || id == 0)
                    return NotFound();
                Product? product = _product.Get(c => c.Id == id); //also work with other fields 
                if (product == null)
                    return NotFound();
                _product.Remove(product);
                _product.Save();
                TempData["success"] = "Product Deleted Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }
    }
}
