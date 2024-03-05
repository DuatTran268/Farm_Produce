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

		public async Task<Category> GetDetailCategoryBySlug(string slug, CancellationToken cancellationToken = default)
		{
			IQueryable<Category> categoryQuery = _context.Set<Category>().Include(p => p.Products);
			{
				if (!string.IsNullOrEmpty(slug))
				{
					categoryQuery = categoryQuery.Where(ct => ct.UrlSlug == slug);
				}
			}
			return await categoryQuery.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IList<T>> GetNLimitCategory<T>(int n, Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			var cateLimit = _context.Set<Category>()
				.Include(p => p.Products)
				.OrderByDescending(p => p.Name)
				.Take(n);
			return await mapper(cateLimit).ToListAsync(cancellationToken);
		}
	}
}
