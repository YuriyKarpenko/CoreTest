using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Model
{
	public class Product : ApiModelBase
    {
		[Required]
        public double Price { get; set; }
		[Required]
        public bool IsAvailable { get; set; }
        public DateTime? DeliveryDate { get; set; }

		[Required]
        public int UnitCode { get; set; }
        public virtual Unit Unit { get; set; }

		[Required]
        public int TypeCode { get; set; }
        public virtual Type Type { get; set; }

		[Required]
        public virtual HashSet<ProductCategories> ProductCategories { get; set; }
    }
}
