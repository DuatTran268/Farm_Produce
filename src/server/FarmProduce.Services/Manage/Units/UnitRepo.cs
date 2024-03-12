using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Services.Extentions;
using Microsoft.EntityFrameworkCore;
using FarmProduce.Core.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Units
{
    public class UnitRepo
    {
        private readonly FarmDbContext _context;

        public UnitRepo(FarmDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Unit>> GetAllAsync(CancellationToken cancellationToken=default) {
            return await _context.Set<Unit>().ToListAsync(cancellationToken);
        }
        public async Task<IPagedList<Unit>> GetAllPagedAsync(IPagingParams pagingParams, CancellationToken cancellationToken=default)
        {
            return await _context.Set<Unit>().ToPagedListAsync(pagingParams, cancellationToken);   
        }
        public async Task<Unit> GetByIdAsync(int id, CancellationToken cancellationToken=default)
        {
            return await _context.Set<Unit>().Where(x=> x.Id== id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
