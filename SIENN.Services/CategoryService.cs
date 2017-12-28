using System;
using System.Collections.Generic;

using SIENN.DbAccess.Model;
using SIENN.DbAccess.Repositories;

namespace SIENN.Services
{
    public class CategoryService : GenericService<Model.Category, Category>
    {
        public CategoryService() : base(new CategoryRepository())
        {

        }
    }
}
