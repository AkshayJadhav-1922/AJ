using Aj.DataAccess.Repository.IRepository;
using AJ.DataAcess.Data;
using AJ.Models;
using AJ.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AJStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        //In tradition .net application, We had to create Object of ApplicationDb Context
        //.net core provides object of db context as we have regitered it in Program.cs
        private readonly ICategoryRepository _category;
        public CategoryController(ICategoryRepository db)
        {
            _category = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Custom Validations
            //if(!obj.Name.IsNullOrEmpty() && !obj.DisplayOrder.ToString().IsNullOrEmpty() && obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name and Display order cannot be exactly same");
            //}
            if (ModelState.IsValid)
            {
                _category.Add(obj);
                TempData["success"] = "Category Created Successfully";
                _category.Save();
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            //Category category = _db.Categories.Find(id);  - Only works for primary key
            //Category? category = _db.Categories.Where(c=> c.Id == id).FirstOrDefault();
            Category? category = _category.Get(c => c.Id == id); //also work with other fields 
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _category.Update(obj);
                _category.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = _category.Get(c => c.Id == id); //also work with other fields 
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null || id == 0)
                    return NotFound();
                Category? category = _category.Get(c => c.Id == id); //also work with other fields 
                if (category == null)
                    return NotFound();
                _category.Remove(category);
                _category.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
    }
}
