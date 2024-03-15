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

namespace FarmProduce.Services.Manage.Carts
{
    public class CartRepo : ICartRepo
    {
        private readonly FarmDbContext _context;

        public CartRepo(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Cart>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<Cart> carts = _context.Set<Cart>();
            return await mapper(carts).ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Cart>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<Cart> carts = _context.Set<Cart>();
            return await mapper(carts).ToPagedListAsync(pagingParams, cancellationToken);
        }
    }
}
