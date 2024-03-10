using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Admins
{
	public interface IAdminRepo
	{
		Task<IList<T>> GetAllAdmin<T>(Func<IQueryable<Admin>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
		
		Task<Admin> GetAdminById(int id, CancellationToken cancellationToken = default);

	
	
	}
}
