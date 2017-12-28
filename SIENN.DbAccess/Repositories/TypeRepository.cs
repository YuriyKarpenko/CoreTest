using Microsoft.EntityFrameworkCore;

using SIENN.DbAccess.Model;

namespace SIENN.DbAccess.Repositories
{
    public class TypeRepository : GenericRepository<Type>
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }

        public TypeRepository() : base(new SiennDbContext())
        {
        }
    }
}
