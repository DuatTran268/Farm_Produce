using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Categories
{
	public interface ICategoriesRepo
	{
		Task<IPagedList<CategoryItem>> GetAllPagingationCategory(IPagingParams pagingParams,string name = null,CancellationToken cancellationToken = default);

		Task<Category> GetCategoryById(int id, CancellationToken cancellationToken = default);

		Task<Category> GetDetailCategoryBySlug(string slug, CancellationToken cancellationToken = default);

		Task<IList<T>> GetNLimitCategory<T>(int n, Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);


		Task<IList<T>> GetLimitCategoryNewest<T>(int n, Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);


		Task<IPagedList<T>> GetListProductsWithSlugOfCategory<T>(ProductQuery query,
		  IPagingParams pagingParams,
		  Func<IQueryable<Product>,
		  IQueryable<T>> mapper,
		  CancellationToken cancellationToken = default);


		// add or update category
		Task<bool> AddOrUpdateAsync(Category category, CancellationToken cancellationToken = default);

		Task<bool> IsCategorySlugExistedAsync(
		int categoryId,
		string slug,
		CancellationToken cancellationToken = default);

		Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

		Task<bool> DeleteCategory(int id, CancellationToken cancellationToken = default);
		
			Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken);
		Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
		Task<bool> AddOrUpdate(Category category, CancellationToken cancellationToken = default);


		Task<IList<CategoryItem>> GetCategoryCombobox(CancellationToken cancellationToken = default);


		Task<int> CountTotalCategoryOfProduct(CancellationToken cancellationToken = default);

	}
}
