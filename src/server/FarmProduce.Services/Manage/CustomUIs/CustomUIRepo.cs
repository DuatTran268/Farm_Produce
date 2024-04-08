using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.CustomUIs
{
    public class CustomUIRepo : ICustomUIRepo
    {
        private readonly FarmDbContext _context;

        public CustomUIRepo(FarmDbContext context)
        {
            _context = context;
        }

        public async Task<IList<T>> GetAllAsync<T>(Func<IQueryable<CustomUI>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<CustomUI> result = _context.Set<CustomUI>();
            return await mapper(result).ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<CustomUI>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<CustomUI> result = _context.Set<CustomUI>();
            return await mapper(result).ToPagedListAsync(pagingParams, cancellationToken);
        }
        public async Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<CustomUI>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<CustomUI>().Remove(result);
                return true;
            }
        }
        public async Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CustomUI>().AnyAsync(x => x.Id != id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<CustomUI>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<CustomUI>().Remove(result);
                return true;
            }
        }
        public async Task<bool> AddOrUpdate(CustomUI custom, CancellationToken cancellationToken = default)
        {
            if (custom.Id > 0)
            {
                _context.Update(custom);
            }
            else
            {
                _context.Add(custom);
            }
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
