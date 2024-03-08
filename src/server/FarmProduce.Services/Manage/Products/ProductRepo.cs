using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Products
{
	public class ProductRepo : IProductRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;


		public ProductRepo (FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IList<T>> GetAllProducts<T>(Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> products = _context.Set<Product>().OrderBy(p => p.Name);
			return await mapper(products).ToListAsync(cancellationToken);

		}

		public async Task<Product> GetDetailProductBySlug(string slug, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> productQuery = _context.Set<Product>()
				.Include(p => p.Discounts)
				.Include(p => p.Images)
				.Include(p => p.Comments)
				.Include(p => p.Carts)
				.Include(p => p.Orders);
			{
				if (!string.IsNullOrEmpty(slug))
				{
					productQuery = productQuery.Where(pd => pd.UrlSlug == slug);
				}
			}
			return await productQuery.FirstOrDefaultAsync(cancellationToken);
		}



		public async Task<IList<T>> GetLitmitProductNewest<T>(int n, Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			var productLimit = _context.Set<Product>()
				.Include(p => p.Discounts)
				.Include(p => p.Images)
				.Include(p => p.Comments)
				.Include(p => p.Carts)
				.Include(p => p.Orders)
				.OrderByDescending(p => p.Id)
				.Take(n);
			return await mapper(productLimit).ToListAsync(cancellationToken);
		}
	}
}
