using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
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

		public async Task<IPagedList<T>> GetAllCategories<T>(Func<IQueryable<Category>, IQueryable<T>> mapper,IPagingParams pagingParams, CancellationToken cancellationToken = default)
		{
			IQueryable<Category> categories = _context.Set<Category>().OrderBy(a => a.Name);
			return await mapper(categories).ToPagedListAsync(pagingParams, cancellationToken);
		}

		public async Task<Category> GetCategoryById(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Category>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
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


		public async Task<IList<T>> GetLimitCategoryNewest<T>(int n, Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			var cateLimit = _context.Set<Category>()
				.Include(p => p.Products)
				.OrderByDescending(p => p.Id)
				.Take(n);
			return await mapper(cateLimit).ToListAsync(cancellationToken);
		}

		public async Task<IPagedList<T>> GetListProductsWithSlugOfCategory<T>(ProductQuery query, IPagingParams pagingParams, Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> productFindQuery = FilterProduct(query);
			IQueryable<T> queryResult = mapper(productFindQuery);
			return await queryResult.ToPagedListAsync(pagingParams, cancellationToken); 

		}
		private IQueryable<Product> FilterProduct(ProductQuery query)
		{
			IQueryable<Product> productQuery = _context.Set<Product>();
			if (!string.IsNullOrWhiteSpace(query.UrlSlug))
			{
				productQuery = productQuery.Where(pd => pd.Category.UrlSlug.Contains(query.UrlSlug));
			}
			return productQuery;
		}

        public async Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Category>().Remove(result);
                return true;
            }
        }
        public async Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>().AnyAsync(x => x.Id != id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Category>().Remove(result);
                return true;
            }
        }
        public async Task<bool> AddOrUpdate(Category category, CancellationToken cancellationToken = default)
        {
            if (category.Id > 0)
            {
                _context.Update(category);
            }
            else
            {
                _context.Add(category);
            }
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
