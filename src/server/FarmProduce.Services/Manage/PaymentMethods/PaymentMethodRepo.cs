using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public async Task<bool> IsIdExisted(int id,CancellationToken cancellationToken = default)
        {
            return await _context.Set<PaymentMethod>().AnyAsync(x => x.Id==id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<PaymentMethod>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<PaymentMethod>().Remove(result);
                return true;
            }
        }
        public async Task<bool> AddOrUpdate(PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
        {
            if (paymentMethod.Id > 0)
            {
                _context.Update(paymentMethod);
            }
            else
            {
                _context.Add(paymentMethod);
            }
            return await _context.SaveChangesAsync() > 0;
        }

		public async Task<IList<PaymentMethodItems>> GetPaymentMethodComboobox(CancellationToken cancellationToken = default)
		{
			IQueryable<PaymentMethod> unit = _context.Set<PaymentMethod>();
			return await unit.OrderBy(t => t.Id)
				.Select(t => new PaymentMethodItems()
				{
					Id = t.Id,
					Name = t.Name,
				}).ToListAsync(cancellationToken);
		}

		
	}
}
