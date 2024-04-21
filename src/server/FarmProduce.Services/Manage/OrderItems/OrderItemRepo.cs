using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.OrderItems
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly FarmDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public OrderItemRepo(FarmDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IList<T>> GetAllOrderItem<T>(Func<IQueryable<OrderItem>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<OrderItem> orderItems = _context.Set<OrderItem>();
            return await mapper(orderItems).ToListAsync(cancellationToken);
        }

        public async Task<OrderItem> GetOrderItemByID(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<OrderItem>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
        public async Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<OrderItem>().AnyAsync(x => x.Id != id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<OrderItem>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<OrderItem>().Remove(result);
                return true;
            }
        }
        public async Task<bool> AddOrUpdate(OrderItem orderItem, CancellationToken cancellationToken = default)
        {
            if (orderItem.Id > 0)
            {
                _context.Update(orderItem);
            }
            else
            {
                _context.Add(orderItem);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task AddRangeAsync(IEnumerable<OrderItem> orderItems)
        {
           
            _context.OrderItems.AddRange(orderItems);
            await _context.SaveChangesAsync();
        }
    }
}
