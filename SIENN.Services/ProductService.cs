using System.Collections.Generic;
using System.Linq;

using m = SIENN.DbAccess.Model;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Model;

namespace SIENN.Services
{
	public class ProductService : GenericService<Product, m.Product>
    {
		private ProductRepository repo => (ProductRepository)_repo;

		public ProductService() : base(new ProductRepository())
        {
        }

		public Product GetInfo(int id)
		{
			var res = repo.GetInfo(id);
			return _mapper.Map<Product>(res);
		}

		public IEnumerable<Product> GetAvailable(int start, int count)
		{
			var res = _repo.GetRange(start, count, i => i.IsAvailable);
			return res.Select(i => _mapper.Map<Product>(i));
		}

		public IEnumerable<Product> GetFiltered(int start, int count, int? categoryId, int? typeId, int? unitId)
		{
			var res = repo.GetFiltered(start, count, categoryId, typeId, unitId);
			return res.Select(i => _mapper.Map<Product>(i));
		}

	}
}
