using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using SIENN.Services;
using SIENN.Services.Model;

namespace SIENN.WebApi.Controllers
{
	public class ProducController : BaseController<Product>
    {
        protected ProductService svc;

        public ProducController(IGenericService<Product> svc) : base(svc)
        {
			this.svc = (ProductService)svc;
        }


		[HttpGet("Available")]
		public IEnumerable<Product> GetAvailable(int start, int count)
		{
			return svc.GetAvailable(start, count);
		}

		[HttpGet("Filter")]
		public IEnumerable<Product> GetFiltered(int start, int count, int? categoryId, int? typeId, int? unitId)
		{
			return svc.GetFiltered(start, count, categoryId, typeId, unitId);
		}

		[HttpGet("Info")]
		public ActionResult GetInfo(int id)
		{
			/*
			Field name			Format
			ProductDescription	(Code) Description
			Price				xx,xx zł
			IsAvailable			Yes = „Available”, No = „Unavailable”
			DeliveryDate		12.12.2012
			CategoriesCount	
			Type				(Code) Description
			Unit				(Code) Description
			 */
			var p = svc.GetInfo(id);

			if (p == null)
				return NotFound();
			else
				return Ok(new
				{
					ProductDescription = p.ToString(),
					Price = p.Price.ToString("C2"),
					IsAvailable = p.IsAvailable ? "Available" : "Unavailable",
					DeliveryDate = p.DeliveryDate?.ToString(@"dd\.MM\.yyyy"),
					CategoriesCount = p.ProductCategories.Count,
					Type = p.Type.ToString(),
					Unit = p.Unit.ToString()
				});
		}

	}
}