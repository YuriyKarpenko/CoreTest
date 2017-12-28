using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIENN.DbAccess.Model
{
	class SiennDbContext : DbContext
	{
		public DbSet<Category> Category { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductCategories> ProductCategories { get; set; }
		public DbSet<Model.Type> Type { get; set; }
		public DbSet<Unit> Unit { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite("Filename=db.sqlite");
		}

		protected override void OnModelCreating(ModelBuilder mb)
		{
			base.OnModelCreating(mb);

			mb.Entity<Category>().HasKey(i => i.Id);
			mb.Entity<Product>().HasKey(i => i.Id);
			mb.Entity<ProductCategories>().HasKey(i => new { i.CategoryId, i.ProductId });
			mb.Entity<Model.Type>().HasKey(i => i.Id);
			mb.Entity<Unit>().HasKey(i => i.Id);

			mb.Entity<Product>()
				.HasOne(i => i.Type)
				.WithMany()
				.HasForeignKey(i => i.TypeId);
			mb.Entity<Product>()
				.HasOne(i => i.Unit)
				.WithMany()
				.HasForeignKey(i => i.UnitId);

			mb.Entity<ProductCategories>()
				.HasOne<Category>(i => i.Category)
				.WithMany()
				.HasForeignKey(i => i.CategoryId);
			mb.Entity<ProductCategories>()
				.HasOne<Product>(i => i.Product)
				.WithMany(i => i.ProductCategories)
				.HasForeignKey(i => i.ProductId);
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

		public IQueryable<Product> Query1()
		{
			var d = DateTime.Today;
			var d1 = new DateTime(d.Year, d.Month, 1);
			var d2 = d1.AddMonths(1);

			var q = Product
				.Where(i => !i.IsAvailable)
				.Where(i => i.DeliveryDate.HasValue && d1 < i.DeliveryDate && i.DeliveryDate > d2);

			return q;
		}

		public IQueryable<Product> Query2()
		{
			var q = Product
				.Include(i => i.ProductCategories)
				.Where(i => i.ProductCategories.Count() > 1);

			return q;
		}

		public IQueryable<object> Query3()
		{
			var q3_group = ProductCategories
			.Include(i => i.Category)
			.Include(i => i.Product)
			.Select(i => new { c = i.Category.Name, p = i.Product })
			.GroupBy(i => i.c)
			;
			var q = q3_group
				.OrderByDescending(i => i.Max(p => p.p.Price))
				.Take(3)
				.Select(i => new
				{
					category = i.Key,
					availableProducts = i.Count(p => p.p.IsAvailable),
					products = i.GroupBy(x => x.p.Name).Select(x => new { name = x.Key, price = x.Average(p => p.p.Price) }),
				})
				//.ToArray()
				;

			return q;
		}

	}
}
