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


		/*
To created data scheme create queries which in turn will deliver following data:
1. Unavailable products which delivery is expected in current month
2. Available products that are assigned to more than one category
3. Top 3 categories with info about numbers of available, assigned products with its mean price in category (top 3 should display categories which mean price is the highest)

3. Лучшие 3 категории с информацией о количестве доступных, назначенных продуктов со средней ценой в категории (верхняя 3 должна отображать категории, которые означают, что цена является самой высокой)
3. Топ 3 категории с информацией о количестве доступных, назначенных продукции с его средняя цена в категории (Топ 3 следует отображать категории, которые означают, Цена самая высокая)
т.е.
получить 3 категории с максимальной ценой продукта, включая:
	1 кол-во доступных продуктов
	2 продукты с их средней ценой в категории
		 */

		private void TestQueries()
		{
			var d = DateTime.Today;
			var d1 = new DateTime(d.Year, d.Month, 1);
			var d2 = d1.AddMonths(1);

			var q1 = _entities
				.Where(i => !i.IsAvailable)
				.Where(i => i.DeliveryDate.HasValue && d1 < i.DeliveryDate && i.DeliveryDate > d2);

			var q2 = _entities
				.Include(i => i.ProductCategories)
				.Where(i => i.ProductCategories.Count > 1);

			var q3_group = _context.Set<ProductCategories>()
				.Include(i => i.Category)
				.Include(i => i.Product)
				.Select(i => new { c = i.Category.Name, p = i.Product })
				.GroupBy(i => i.c)
				//.ToArray()
				;
			var q3 = q3_group
				.OrderByDescending(i => i.Max(p => p.p.Price))
				.Select(i => new
				{
					category = i.Key,
					availableProducts = i.Count(p => p.p.IsAvailable),
					products = i.GroupBy(x => x.p.Name).Select(x => new { name = x.Key, price = x.Average(p => p.p.Price) }),
				})
				.Take(3)
				.ToArray()
				;

		}
	}
}
