using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.OrderItems
{
    public interface IOrderItemRepo
    {

        Task<IList<T>> GetAllOrderItem<T>(Func<IQueryable<OrderItem>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<OrderItem> GetOrderItemByID(int id, CancellationToken cancellationToken = default);
        Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddOrUpdate(OrderItem orderItem, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<OrderItem> orderItems);


    }
}
