using System;

namespace SIENN.DbAccess.Model
{
	public class ProductCategories
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }

		public Category Category { get; set; }
		public Product Product { get; set; }
	}
}
