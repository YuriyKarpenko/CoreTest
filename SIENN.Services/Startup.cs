using Microsoft.Extensions.DependencyInjection;

using m = SIENN.Services.Model;

namespace SIENN.Services
{
	public class Startup
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddTransient<IGenericService<m.Category>>(i => new CategoryService());
			services.AddTransient<IGenericService<m.Product>>(i => new ProductService());
			services.AddTransient<IGenericService<m.Type>>(i => new TypeService());
			services.AddTransient<IGenericService<m.Unit>>(i => new UnitService());

		}
	}
}