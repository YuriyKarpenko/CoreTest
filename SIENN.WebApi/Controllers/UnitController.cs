using SIENN.Services;
using SIENN.Services.Model;

namespace SIENN.WebApi.Controllers
{
    public class UnitController : BaseController<Unit>
    {
        public UnitController(IGenericService<Unit> svc) : base(svc)
        {
        }
    }
}
