using AJ.DataAcess.Data;
using AJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository  //remeber the format: we also have to inherit Repository<Category> which will give us all other definations
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        //Remember We have other definations available in Repository.vb only we have to define other 2 methods
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
