using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Admins
{
	public class AdminRepo :IAdminRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;
		public AdminRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IList<T>> GetAllAdmin<T>(Func<IQueryable<Admin>, IQueryable<T>> mapper, CancellationToken cancellationToken)
		{
			IQueryable<Admin> admins = _context.Set<Admin>().OrderBy(a => a.Name);
			return await mapper(admins).ToListAsync(cancellationToken);
		}

		public async Task<Admin> GetAdminById(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Admin>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		}

	}
}
