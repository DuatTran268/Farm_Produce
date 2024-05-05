using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FarmProduce.Core.DTO.ServiceResponses;

namespace FarmProduce.Services.Manage.Orders
{
    public interface IOrderRepo
    {
        Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
        Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
		Task<bool> DeleteOrder(int id, CancellationToken cancellationToken = default);

		Task<bool> AddOrUpdate(Order order, CancellationToken cancellationToken = default);
        Task<GeneralResponse> CreateOrder(DetailOrder orderDTO);
        Task<OrderDetailDTO> GetOrderById(int id, CancellationToken cancellationToken = default);
	}
}
