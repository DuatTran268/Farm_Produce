﻿using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Orders
{
    public interface IOrderRepo
    {
        Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
    }
}