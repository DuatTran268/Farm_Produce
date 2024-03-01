
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using Mapster;

namespace FarmProduct.WebApi.Mapsters
{
	public class MapsterConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Admin, AdminDto>();


		}
	}

}
