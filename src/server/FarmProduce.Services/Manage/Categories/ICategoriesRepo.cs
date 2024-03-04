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



	}
}
