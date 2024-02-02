﻿using AJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Category category);
        void Save();
    }
}
