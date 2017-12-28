using AutoMapper;
using AutoMapper.Configuration;

using md = SIENN.DbAccess.Model;
using ma = SIENN.Services.Model;

namespace SIENN.Services
{
	class Mapper
	{
		class ToApi : Profile
		{
			public ToApi() : base(nameof(ToApi))
			{
				CreateMap<md.Product, ma.Product>()
					.ForMember(i => i.Code, c => c.MapFrom(i => i.Id))
					.ForMember(i => i.TypeCode, c => c.MapFrom(i => i.TypeId))
					.ForMember(i => i.UnitCode, c => c.MapFrom(i => i.UnitId))
					.AfterMap((s, d) =>
					{
						d.Code = s.Id;
						d.Description = s.Name;
					});

				CreateMap<md.ProductCategories, ma.ProductCategories>()
					.ForMember(i => i.CategoryCode, c => c.MapFrom(i => i.CategoryId))
					.ForMember(i => i.ProductCode, c => c.MapFrom(i => i.ProductId));

				CreateMap<md.Category, ma.Category>()
					.AfterMap((s, d) =>
					{
						d.Code = s.Id;
						d.Description = s.Name;
					});

				CreateMap<md.Type, ma.Type>()
					.AfterMap((s, d) =>
					{
						d.Code = s.Id;
						d.Description = s.Name;
					});

				CreateMap<md.Unit, ma.Unit>()
					.AfterMap((s, d) =>
					{
						d.Code = s.Id;
						d.Description = s.Name;
					});

			}
		}

		class ToDb : Profile
		{
			public ToDb() : base(nameof(ToDb))
			{
				CreateMap<ma.Product, md.Product>()
					//.ForMember(i => i.Id, c => c.MapFrom(i => i.Code))
					.ForMember(i => i.TypeId, c => c.MapFrom(i => i.TypeCode))
					.ForMember(i => i.UnitId, c => c.MapFrom(i => i.UnitCode))
					.AfterMap((s, d) =>
					{
						d.Id = s.Code;
						d.Name = s.Description;
					});

				CreateMap<ma.ProductCategories, md.ProductCategories>()
					.ForMember(i => i.CategoryId, c => c.MapFrom(i => i.CategoryCode))
					.ForMember(i => i.ProductId, c => c.MapFrom(i => i.ProductCode));

				CreateMap<ma.Category, md.Category>()
					.AfterMap((s, d) =>
					{
						d.Id = s.Code;
						d.Name = s.Description;
					});

				CreateMap<ma.Type, md.Type>()
					.AfterMap((s, d) =>
					{
						d.Id = s.Code;
						d.Name = s.Description;
					});

				CreateMap<ma.Unit, md.Unit>()
					.AfterMap((s, d) =>
					{
						d.Id = s.Code;
						d.Name = s.Description;
					});
			}
		}


		private static IMapper _mapper;

		public static IMapper Get()
		{
			if (_mapper == null)
			{
				var ce = new MapperConfigurationExpression();
				ce.AddProfile<ToApi>();
				ce.AddProfile<ToDb>();
				_mapper = new MapperConfiguration(ce).CreateMapper();
			}
			return _mapper;
		}
	}
}
