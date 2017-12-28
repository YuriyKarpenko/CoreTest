using System.Collections.Generic;
using System.Linq;

using SIENN.DbAccess.Repositories;

namespace SIENN.Services
{
	public class GenericService<TApiModel, TDbModel> : IGenericService<TApiModel>
		where TApiModel : class, Model.IApiModelBase
		where TDbModel : class, DbAccess.Model.IDtoBase
	{
		protected readonly IGenericRepository<TDbModel> _repo;
		protected AutoMapper.IMapper _mapper => Mapper.Get();

		public GenericService(IGenericRepository<TDbModel> repo)
		{
			_repo = repo;
		}

		public TApiModel Get(int id)
		{
			var res = _repo.Get(id);
			return _mapper.Map<TApiModel>(res);
		}

		public IEnumerable<TApiModel> GetRange(int start, int count)
		{
			var res = _repo.GetRange(start, count);
			return res.Select(i => _mapper.Map<TApiModel>(i));
		}

		public int Insert(TApiModel item)
		{
			var i = _mapper.Map<TDbModel>(item);
			try
			{
				_repo.Add(i);
				return _repo.Save();
			}
			finally
			{
				_mapper.Map(i, item);
			}
		}

		public int Update(TApiModel item)
		{
			var i = _mapper.Map<TDbModel>(item);
			_repo.Update(i);
			return _repo.Save();
		}

		public int Remove(int id)
		{
			var i = _repo.Get(id);
			_repo.Remove(i);
			return _repo.Save();
		}
	}
}
