﻿using Aj.DataAccess.Repository.IRepository;
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
        public ProductController(IProductRepository db, ICategoryRepository ct)
        {
            _product = db;
            _category = ct;
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
                _product.Add(obj.Product);
                TempData["success"] = "Product Created Successfully";
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
