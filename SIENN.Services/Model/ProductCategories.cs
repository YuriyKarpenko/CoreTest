using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Model
{
    public class ProductCategories
    {
		[Required]
		public int ProductCode { get; set; }
		[Required]
		public int CategoryCode { get; set; }
    }
}
