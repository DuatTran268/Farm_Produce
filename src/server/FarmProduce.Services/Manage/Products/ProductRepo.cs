using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FarmProduce.Services.Manage.Products
{
	public class ProductRepo : IProductRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;


		public ProductRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IPagedList<T>> GetAllProducts<T>(Func<IQueryable<Product>, IQueryable<T>> mapper, ProductQuery productQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> products = FilterProduct(productQuery);
			return await mapper(products).ToPagedListAsync(pagingParams, cancellationToken);

		}


		private IQueryable<Product> FilterProduct(ProductQuery productQuery)
		{
			IQueryable<Product> products = _context.Set<Product>().Include(x=>x.Images);
			if (!String.IsNullOrWhiteSpace(productQuery.UrlSlug))
			{
				products = products.Where(x => x.UrlSlug.Contains(productQuery.UrlSlug));
			}
			if (!String.IsNullOrWhiteSpace(productQuery.Name))
			{
				products = products.Where(x => x.Name.Contains(productQuery.Name));
			}
			if (productQuery.Status == true)
			{
				products = products.Where(p => p.Status);
			}
			if (productQuery.Status == false)
			{
				products = products.Where(p => !p.Status);
			}
			return products;
		}
		public async Task<bool> DeleteWithSlugAsync(string slug, CancellationToken cancellationToken)
		{
			var result = await _context.Set<Product>().Where(x => x.UrlSlug == slug).FirstOrDefaultAsync();
			if (result is null)
			{
				return false;
			}
			else
			{
				_context.Set<Product>().Remove(result);
				return true;
			}
		}
		public async Task<bool> IsSlugProductExisted(int id, string urlSlug, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Product>().AnyAsync(x => x.Id != id && x.UrlSlug == urlSlug);
		}
		public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
		{
			return await _context.Products.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken) > 0;

			//var result = await _context.Set<Product>().Where(x => x.Id == id).FirstOrDefaultAsync();
			//if (result is null)
			//{
			//	return false;
			//}
			//else
			//{
			//	_context.Set<Product>().Remove(result);
			//	return true;
			//}
		}
		public async Task<bool> AddOrUpdateProduct(Product product, CancellationToken cancellationToken = default)
		{
			if (product.Id > 0)
			{
				_context.Update(product);
			}
			else
			{
				_context.Add(product);
			}

			// Thực hiện lưu các thay đổi vào cơ sở dữ liệu và kiểm tra xem có thay đổi nào được lưu không
			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}


		public async Task<Product> GetProductById(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Product>().Include(p => p.Images).Include(p => p.Unit).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
		}

		public async Task<Product> GetDetailProductBySlug(string slug, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> productQuery = _context.Set<Product>()
				.Include(p => p.Images)
				.Include(p => p.Comments)
				.Include(p => p.OrderItems);
			{
				if (!string.IsNullOrEmpty(slug))
				{
					productQuery = productQuery.Where(pd => pd.UrlSlug == slug);
				}
			}
			return await productQuery.FirstOrDefaultAsync(cancellationToken);
		}

		// get id and slug of product for comment
		public async Task<Product> GetIdSlugOfProductForComment(string slug, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> productQuery = _context.Set<Product>();
			{
				if (!string.IsNullOrEmpty(slug))
				{
					productQuery = productQuery.Where(pq => pq.UrlSlug == slug);
				}
			}
			return await productQuery.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
		{
			product.UrlSlug = product.Name.GenerateSlug();
			_context.Update(product);
			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}
		public async Task<bool> AddAsync(Product product, CancellationToken cancellationToken = default)
		{
			product.UrlSlug = product.Name.GenerateSlug();
			_context.Add(product);
			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}
		public async Task<IList<T>> GetLitmitProductNewest<T>(int n, Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			var productLimit = _context.Set<Product>()
				.Include(p => p.Images)
				.Include(p => p.Comments)
				.Include(p => p.OrderItems)
				.OrderByDescending(p => p.Id)
				.Take(n);
			return await mapper(productLimit).ToListAsync(cancellationToken);
		}
		public async Task<IPagedList<T>> GetCommentWithPaged<T>(CommentQuery query, IPagingParams pagingParams, Func<IQueryable<Comment>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<Comment> cmtFindQuery = FilterComment(query);
			IQueryable<T> queryResult = mapper(cmtFindQuery);
			return await queryResult.ToPagedListAsync(pagingParams, cancellationToken);

		}

		private IQueryable<Comment> FilterComment(CommentQuery query)
		{
			IQueryable<Comment> cmtQuery = _context.Set<Comment>();
			if (!string.IsNullOrWhiteSpace(query.UrlSlug))
			{
				cmtQuery = cmtQuery.Where(p => p.Product.UrlSlug.Contains(query.UrlSlug));
			}
			return cmtQuery;
		}
		public async Task<bool> AddProductWithImages(Product product, List<Image> images, CancellationToken cancellationToken = default)
		{

			foreach (var image in images)
			{
				product.Images.Add(image);
			}


			if (product.Id > 0)
			{
				_context.Update(product);
			}
			else
			{
				_context.Add(product);
			}


			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}

		public async Task<IList<ProductItem>> GetProductCombobox(CancellationToken cancellationToken = default)
		{
			IQueryable<Product> topics = _context.Set<Product>();
			return await topics.OrderBy(t => t.Id)
				.Select(t => new ProductItem()
				{
					Id = t.Id,
					Name = t.Name,
				}).ToListAsync(cancellationToken);
		}

		public async Task<bool> IncreaseViewCountAsync(string slug, CancellationToken cancellationToken = default)
		{
			var productView = await _context.Set<Product>()
				 .Where(p => p.UrlSlug == slug)
				 .FirstOrDefaultAsync(cancellationToken);
			productView.ViewCount = productView.ViewCount + 1;
			_context.Update(productView);
			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}
	}
}
