using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.OrderStatuses
{
	public class OrderStatusRepo : IOrderStatusRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public OrderStatusRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IList<T>> GetAllOrderStatus<T>(Func<IQueryable<OrderStatus>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<OrderStatus> orderStatuses = _context.Set<OrderStatus>();
			return await mapper(orderStatuses).ToListAsync(cancellationToken);
		}

		public async Task<OrderStatus> GetOrderStatusByID(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<OrderStatus>().FirstOrDefaultAsync( o => o.Id == id,cancellationToken);
		}
	}
}
