using Microsoft.EntityFrameworkCore;

using SIENN.DbAccess.Model;

namespace SIENN.DbAccess.Repositories
{
    public class UnitRepository : GenericRepository<Unit>
    {
        public UnitRepository(DbContext context) : base(context)
        {
        }

        public UnitRepository() : base(new SiennDbContext())
        {
        }
    }
}
