using Microsoft.AspNetCore.Mvc;

using SIENN.Services;
using SIENN.Services.Model;

namespace SIENN.WebApi.Controllers
{
	//[ResponseCache(NoStore = true)]
	public class CategoryController : BaseController<Category>
    {
		public CategoryController(IGenericService<Category> svc) : base(svc)
		{
		}
	}
}
