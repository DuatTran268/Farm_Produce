using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.OrderStatuses
{
	public interface IOrderStatusRepo
	{
		Task<IList<T>> GetAllOrderStatus<T>(Func<IQueryable<OrderStatus>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
		Task<OrderStatus> GetOrderStatusByID(int id, CancellationToken cancellationToken = default);
        Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddOrUpdate(OrderStatus orderStatus, CancellationToken cancellationToken = default);

		Task<IList<OrderStatusItem>> GetOrderStatusCombobox(CancellationToken cancellationToken = default);

	}
}
