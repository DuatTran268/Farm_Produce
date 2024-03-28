﻿using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Discounts
{
	public interface IDiscountRepo
	{
		Task<IPagedList<T>> GetAllDiscount<T>(Func<IQueryable<Discount>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
	
		Task<Discount> GetDiscountByID(int id, CancellationToken cancellationToken = default);

	}
}