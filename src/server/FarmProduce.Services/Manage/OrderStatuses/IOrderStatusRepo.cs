using FarmProduce.Core.Entities;
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

	}
}
