using Aj.DataAccess.Repository.IRepository;
using AJ.DataAcess.Data;
using AJ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AJStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //In tradition .net application, We had to create Object of ApplicationDb Context
        //.net core provides object of db context as we have regitered it in Program.cs
        private readonly IProductRepository _product;
        public ProductController(IProductRepository db)
        {
            _product = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //Custom Validations
            //if(!obj.Name.IsNullOrEmpty() && !obj.DisplayOrder.ToString().IsNullOrEmpty() && obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name and Display order cannot be exactly same");
            //}
            if (ModelState.IsValid)
            {
                _product.Add(obj);
                TempData["success"] = "Product Created Successfully";
                _product.Save();
                return RedirectToAction("Index", "Product");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Product? product = _product.Get(c => c.Id == id); //also work with other fields 
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _product.Update(obj);
                _product.Save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

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
