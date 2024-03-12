using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
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

namespace FarmProduce.Services.Manage.Customers
{
    public class CustomRepo:ICustomRepo
    {
        private readonly FarmDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public CustomRepo(FarmDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public async Task<IList<Customer>> GetAllAsync(CustomerQuery query,CancellationToken cancellationToken= default)
        {
            var filter = Filter(query);
            return await filter.ToListAsync();
        }
        public async Task<IPagedList<Customer>> GetAllPageAsync(CustomerQuery query, IPagingParams pagingParams, CancellationToken cancellationToken= default)
        {
            var filter = Filter(query);
            return await filter.ToPagedListAsync(pagingParams, cancellationToken);
        }
        private IQueryable<Customer> Filter(CustomerQuery query)
        {
            var cusQuery = _context.Set<Customer>().AsQueryable();
           if(!string.IsNullOrWhiteSpace(query.Address) && !string.IsNullOrWhiteSpace(query.Phone) && !string.IsNullOrWhiteSpace(query.Name))
            {
                cusQuery = cusQuery.Where(x => x.Address.Contains(query.Address))
               .Where(x => x.Email.Contains(query.Email)).Where(x => x.Name.Contains(query.Name));
            }
           return cusQuery;
        }      

    }
}
