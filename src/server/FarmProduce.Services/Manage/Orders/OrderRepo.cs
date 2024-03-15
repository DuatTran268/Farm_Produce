using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Orders
{
    public class OrderRepo : IOrderRepo
    {
        private readonly FarmDbContext _context;

        public OrderRepo(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<Order> orders = _context.Set<Order>();
            return await mapper(orders).ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<Order> orders = _context.Set<Order>();
            return await mapper(orders).ToPagedListAsync(pagingParams, cancellationToken);
        }
    }
}
