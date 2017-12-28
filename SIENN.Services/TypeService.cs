using m = SIENN.DbAccess.Model;
using SIENN.DbAccess.Repositories;

namespace SIENN.Services
{
    public class TypeService : GenericService<Model.Type, m.Type>
    {
        public TypeService() : base(new TypeRepository())
        {
        }
    }
}
