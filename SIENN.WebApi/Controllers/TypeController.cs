using SIENN.Services;
using SIENN.Services.Model;

namespace SIENN.WebApi.Controllers
{
	public class TypeController : BaseController<Type>
	{
		public TypeController(IGenericService<Type> svc) : base(svc)
		{
		}
	}
}
