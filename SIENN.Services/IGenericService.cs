using System.Collections.Generic;

using SIENN.Services.Model;

namespace SIENN.Services
{
	public interface IGenericService<T> where T : class, IApiModelBase
	{
		T Get(int id);
		IEnumerable<T> GetRange(int start, int count);
		int Insert(T item);
		int Update(T item);
		int Remove(int id);
	}
}