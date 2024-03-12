﻿using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Categories
{
	public interface ICategoriesRepo
	{
		Task<IList<T>> GetAllCategories<T>(Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

		Task<Category> GetDetailCategoryBySlug(string slug, CancellationToken cancellationToken = default);

		Task<IList<T>> GetNLimitCategory<T>(int n, Func<IQueryable<Category>,IQueryable<T>> mapper, CancellationToken cancellationToken = default);
	

		Task<IList<T>> GetLimitCategoryNewest<T>(int n, Func<IQueryable<Category>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
	
	
	}
}