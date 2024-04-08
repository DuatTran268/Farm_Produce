using FarmProduce.Core.Contracts;
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

namespace FarmProduce.Services.Manage.Discounts
{
	public class DiscountRepo : IDiscountRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public DiscountRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IPagedList<T>> GetAllDiscount<T>(Func<IQueryable<Discount>, IQueryable<T>> mapper,IPagingParams pagingParams ,CancellationToken cancellationToken = default)
		{
			IQueryable<Discount> discounts = _context.Set<Discount>();
			return await mapper(discounts).ToPagedListAsync(pagingParams, cancellationToken);
		}

		public async Task<Discount> GetDiscountByID(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Discount>().FirstOrDefaultAsync(d => d.Id == id, cancellationToken); ;

		}
        public async Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Discount>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Discount>().Remove(result);
                return true;
            }
        }
        public async Task<bool> IsIdExisted(int id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Discount>().AnyAsync(x => x.Id != id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Discount>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Discount>().Remove(result);
                return true;
            }
        }
        public async Task<bool> AddOrUpdate(Discount discount, CancellationToken cancellationToken = default)
        {
            if (discount.Id > 0)
            {
                _context.Update(discount);
            }
            else
            {
                _context.Add(discount);
            }
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
