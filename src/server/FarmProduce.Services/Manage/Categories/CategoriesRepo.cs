using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Categories
{
	public class CategoriesRepo : ICategoriesRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public CategoriesRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IList<T>> GetAllCategories<T>(Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<Category> categories = _context.Set<Category>().OrderBy(a => a.Name);
			return await mapper(categories).ToListAsync(cancellationToken);
		}
	}
}
