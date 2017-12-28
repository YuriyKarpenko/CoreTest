using System;
using System.Collections.Generic;

namespace SIENN.DbAccess.Model
{
	public class Product : DtoBase
    {
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        public virtual HashSet<ProductCategories> ProductCategories { get; set; }
    }
}
