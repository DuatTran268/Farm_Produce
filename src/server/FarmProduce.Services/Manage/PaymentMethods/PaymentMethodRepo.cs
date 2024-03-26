﻿using FarmProduce.Core.Contracts;
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

namespace FarmProduce.Services.Manage.PaymentMethods
{
	public class PaymentMethodRepo : IPaymentMethodRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public PaymentMethodRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}


		public async Task<IPagedList<T>> GetAllPaymentMethod<T>(Func<IQueryable<PaymentMethod>, IQueryable<T>> mapper,IPagingParams pagingParams ,CancellationToken cancellationToken = default)
		{
			IQueryable<PaymentMethod> paymentStatus = _context.Set<PaymentMethod>();
			return await mapper(paymentStatus).ToPagedListAsync(pagingParams, cancellationToken);
		}

		public async Task<PaymentMethod> GetPaymentMethodById(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<PaymentMethod>().FirstOrDefaultAsync(pm => pm.Id == id, cancellationToken);
		}
	}
}
