﻿using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SlugGenerator;
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

		public async Task<IPagedList<T>> GetAllProducts<T>(Func<IQueryable<Product> ,IQueryable<T>> mapper,ProductQuery productQuery ,IPagingParams pagingParams ,CancellationToken cancellationToken = default)
		{
			IQueryable<Product> products = FilterProduct(productQuery);
			return await mapper(products).ToPagedListAsync(pagingParams,cancellationToken);

		}
		private IQueryable<Product> FilterProduct(ProductQuery productQuery)
		{
			IQueryable<Product> products = _context.Set<Product>();
			if (!String.IsNullOrWhiteSpace(productQuery.UrlSlug))
			{
				products = products.Where(x => x.UrlSlug.Contains(productQuery.UrlSlug));
			}
            if (!String.IsNullOrWhiteSpace(productQuery.Name))
            {
                products = products.Where(x => x.Name.Contains(productQuery.Name));
            }
			return products;
        }
		public async Task<bool> DeleteWithSlugAsync(string slug,CancellationToken cancellationToken)
		{
			var result = await _context.Set<Product>().Where(x=>x.UrlSlug== slug).FirstOrDefaultAsync();
			if(result is null)
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
            var result = await _context.Set<Product>().Where(x => x.Id == id).FirstOrDefaultAsync();
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
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task <Product> GetProductById(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
		}

		public async Task<Product> GetDetailProductBySlug(string slug, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> productQuery = _context.Set<Product>()
				.Include(p => p.Discounts)
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

		public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
		{
			product.UrlSlug = product.Name.GenerateSlug();
			_context.Update(product);
			return await _context.SaveChangesAsync(cancellationToken) >0;
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
				.Include(p => p.Discounts)
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
			return await queryResult.ToPagedListAsync(pagingParams ,cancellationToken);

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

	}
}
