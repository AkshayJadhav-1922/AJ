using AJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category> //remember: now we know for which class we are writing repositoy interface
    {
        //As we know we have to add Save and Update as extra method which will be specifically for Category related Operations
        void Update(Category category);
        void Save();
    }
}
