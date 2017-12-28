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
	}
}
