﻿using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static FarmProduce.Core.DTO.ServiceResponses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Order>, IQueryable<T>> mapper, OrderQuery orderQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
			//IQueryable<Order> orders = _context.Set<Order>().Include(x=>x.OrderItems);
			IQueryable<Order> orders = FilterOrder(orderQuery);
			return await mapper(orders).ToPagedListAsync(pagingParams, cancellationToken);
        }
		private IQueryable<Order> FilterOrder(OrderQuery orderQuery)
		{
			IQueryable<Order> orders = _context.Set<Order>();
			if (!String.IsNullOrWhiteSpace(orderQuery.Name))
			{
				orders = orders.Where(x => x.ApplicationUser.Name.Contains(orderQuery.Name));
			}

			if (orderQuery.Id > 0)
			{
				orders = orders.Where(o => o.Id == orderQuery.Id);
			}

			return orders;
		}

		public async Task<bool> IsIdExisted(int id,CancellationToken cancellationToken = default)
        {
            return await _context.Set<Order>().AnyAsync(x => x.Id != id);
        }

		public async Task<OrderUpdateDTO> GetOrderById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _context.Orders
                                .Include(x=>x.ApplicationUser)
                                .Include(x=>x.PaymentMethod)
                                .Include(x=>x.OrderStatus)
                                .Include(x=>x.Discount)
                                .Include(x=>x.OrderItems)
                                .ThenInclude(o=>o.Product)
                                .Where(x=>x.Id == id).FirstOrDefaultAsync();
            var order = new OrderUpdateDTO()
            {
                Id = result.Id,
                TotalPrice = result.TotalPrice,
				OrderStatusId = result.OrderStatusId,
				CodeNameDiscount = result.Discount.CodeName,
                DateOrder = result.DateOrder,
                PaymentMethodName = result.PaymentMethod.Name,
                UserName = result.ApplicationUser.Name,
                Address = result.ApplicationUser.Address,
                PhoneNumber = result.ApplicationUser.PhoneNumber,
                OrderItems = result.OrderItems.Select(x => new OrderItemDetailDTO
                {
                    Id = x.Id,
                    Price = x.Product.Price,
                    Quantity = x.Quantity,
                    ProductName = x.Product.Name,
                }).ToList()
            };
            return order;
                              
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
        
        public async Task<GeneralResponse> UpdateOrder(OrderUpdateDTO orderDetailDTO)
        {
            if(orderDetailDTO ==null)
            {
                return new GeneralResponse(false, "Model is empty");
            }
            var order  = await _context.Orders
                .Include(x=>x.ApplicationUser)
                .Include(x=>x.PaymentMethod)
                .Include(x=>x.OrderStatus)
                .Include(x=>x.Discount)
                .Include(x=>x.OrderItems)
                .ThenInclude(x=>x.Product)
                .Where(x=>x.Id == orderDetailDTO.Id).FirstOrDefaultAsync();
            if(order == null)
            {
                return new GeneralResponse(false, "Order not found");
            }
            order.Id = orderDetailDTO.Id;
            order.ApplicationUser.Address = orderDetailDTO.Address;
            order.ApplicationUser.PhoneNumber = orderDetailDTO.PhoneNumber;
            order.OrderStatusId = orderDetailDTO.OrderStatusId;
            order.PaymentMethod.Name = orderDetailDTO.PaymentMethodName;
            order.Discount.CodeName = orderDetailDTO.CodeNameDiscount;
            //order.OrderItems = orderDetailDTO.OrderItems.Select(x=> new OrderItem
            //{
            //    Id = x.Id,
            //    Quantity = x.Quantity,
            //    OrderId= order.Id
            //}).ToList();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "orders updated successfully");

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

		public async Task<int> CountTotalOrder(CancellationToken cancellationToken = default)
		{
			return await _context.Set<Order>().CountAsync(cancellationToken);
		}
	}
}
