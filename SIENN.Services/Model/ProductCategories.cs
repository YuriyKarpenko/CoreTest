using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SIENN.Services.Model
{
	[DebuggerDisplay("p:{ProductCode} c:{CategoryCode}")]
	public class ProductCategories
    {
		[Required]
		public int ProductCode { get; set; }
		[Required]
		public int CategoryCode { get; set; }
    }
}
