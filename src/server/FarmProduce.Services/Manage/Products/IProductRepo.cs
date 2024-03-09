using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Products
{
	public interface IProductRepo
	{

		Task<IList<T>> GetAllProducts<T>(Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

		Task<Product> GetDetailProductBySlug(string slug, CancellationToken cancellationToken = default);

		Task<IList<T>> GetLitmitProductNewest<T>(int n, Func<IQueryable<Product>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

		// get comment have paged
		Task<IPagedList<T>> GetCommentWithPaged<T>(CommentQuery query,
		  IPagingParams pagingParams,
		  Func<IQueryable<Comment>,
		  IQueryable<T>> mapper,
		  CancellationToken cancellationToken = default);

	}
}
