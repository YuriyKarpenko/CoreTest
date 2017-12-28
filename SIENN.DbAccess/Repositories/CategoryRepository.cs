using Microsoft.EntityFrameworkCore;

using SIENN.DbAccess.Model;

namespace SIENN.DbAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public CategoryRepository() : base(new SiennDbContext())
        {
        }
    }
}
