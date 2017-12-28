using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SIENN.Services;
using SIENN.Services.Model;

namespace SIENN.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	[ResponseCache(NoStore = true)]
	public abstract class BaseController<T> : Controller where T : class, IApiModelBase
	{
		protected readonly IGenericService<T> _svc;

		public BaseController(IGenericService<T> svc)
		{
			_svc = svc;
		}

		// GET: api/items
		[HttpGet]
		public IEnumerable<T> GetItems(int start, int count)
		{
			return _svc.GetRange(start, count);
		}

		// GET: api/items/5
		[HttpGet("{id}")]
		public IActionResult GetItem([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var item = _svc.Get(id);

			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		// PUT: api/items/5
		[HttpPut("{id}")]
		public IActionResult PutItem([FromRoute] int id, [FromBody] T item)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != item.Code)
			{
				return BadRequest();
			}

			try
			{
				_svc.Update(item);
			}
			catch (DbUpdateConcurrencyException)
			{
				return NotFound();
			}

			return Ok(item);
		}

		// POST: api/items
		[HttpPost]
		public IActionResult PostItem([FromBody] T item)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_svc.Insert(item);

			return CreatedAtAction("GetItem", new { id = item.Code }, item);
		}

		// DELETE: api/items/5
		[HttpDelete("{id}")]
		public IActionResult DeleteItem([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (_svc.Remove(id) == 0)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}