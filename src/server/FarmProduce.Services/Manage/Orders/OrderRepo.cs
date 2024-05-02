﻿using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FarmProduce.Core.DTO.ServiceResponses;

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
            IQueryable<Order> orders = _context.Set<Order>().Include(x=>x.OrderItems);
            return await mapper(orders).ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<Order> orders = _context.Set<Order>().Include(x=>x.OrderItems);
            return await mapper(orders).ToPagedListAsync(pagingParams, cancellationToken);
        }
        public async Task<bool> IsIdExisted(int id,CancellationToken cancellationToken = default)
        {
            return await _context.Set<Order>().AnyAsync(x => x.Id != id);
        }

		public async Task<Order> GetOrderById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Order>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public async Task<bool> AddOrUpdate(Order order, CancellationToken cancellationToken = default)
        {
            if (order.Id > 0)
            {
                _context.Update(order);
            }
            else
            {
                _context.Add(order);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<GeneralResponse> CreateOrder(DetailOrder orderDTO)
        {
            if (orderDTO is null)
            {
                return new GeneralResponse(false, "Order data is empty");
            }
            var newOrder = new DetailOrder
            {

                DateOrder = orderDTO.DateOrder,
                TotalPrice = orderDTO.TotalPrice,
                OrderStatusId = orderDTO.OrderStatusId,
                ApplicationUserId = orderDTO.ApplicationUserId,
                PaymentMethodId = orderDTO.PaymentMethodId,
                OrderItems = orderDTO.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()

            };
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Order created successfully");

        }

		public async Task<bool> DeleteOrder(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Orders.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken) > 0;
		}

	}
}
