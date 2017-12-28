using SIENN.DbAccess.Model;
using SIENN.DbAccess.Repositories;

namespace SIENN.Services
{
	public class UnitService : GenericService<Model.Unit, Unit>
	{
		public UnitService() : base(new UnitRepository())
		{
		}
	}
}
