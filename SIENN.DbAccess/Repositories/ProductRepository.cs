using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using SIENN.DbAccess.Model;

namespace SIENN.DbAccess.Repositories
{
	public class ProductRepository : GenericRepository<Product>
	{
		public ProductRepository(DbContext context) : base(context)
		{
		}

		public ProductRepository() : base(new SiennDbContext())
		{
		}


		public Product GetInfo(int id)
		{
			return _entities
				.Include(i => i.ProductCategories)
				.Include(i => i.Type)
				.Include(i => i.Unit)
				.FirstOrDefault(i => i.Id == id);
		}

		public IEnumerable<Product> GetFiltered(int start, int count, int? categoryId, int? typeId, int? unitId)
		{
			var query = _entities.AsQueryable();

			if (categoryId.HasValue)
				query = query
				.Include(i => i.ProductCategories)
				.Where(i => i.ProductCategories.Any(x => x.CategoryId == categoryId));

			if (typeId.HasValue)
				query = query.Where(i => i.TypeId == typeId);

			if (unitId.HasValue)
				query = query.Where(i => i.UnitId == unitId);

			return query.Skip(start).Take(count);
		}

	}
}
