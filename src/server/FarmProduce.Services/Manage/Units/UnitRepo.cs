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
using FarmProduce.Core.DTO;

namespace FarmProduce.Services.Manage.Units
{
	public class UnitRepo : IUnitRepo
    {
        private readonly FarmDbContext _context;

        public UnitRepo(FarmDbContext context)
        {
            _context = context;
        }
        public async Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Unit>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> units = _context.Set<Unit>();
            return await mapper(units).ToListAsync(cancellationToken);

        }
        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Unit>, IQueryable<T>> mapper,IPagingParams pagingParams ,CancellationToken cancellationToken = default)
        {
            IQueryable<Unit> units = _context.Set<Unit>();
            return await mapper(units).ToPagedListAsync(pagingParams,cancellationToken);

        }
        public async Task<Unit> GetByIdAsync(int id, CancellationToken cancellationToken=default)
        {
            return await _context.Set<Unit>().Where(x=> x.Id== id).FirstOrDefaultAsync(cancellationToken);
        }

		public async Task<IPagedList<UnitItem>> GetPagedUnit(IPagingParams pagingParams, string name = null, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Unit>()
				.AsNoTracking()
				.WhereIf(!string.IsNullOrWhiteSpace(name),
				x => x.Name.Contains(name))
				.Select(d => new UnitItem()
				{
					Id = d.Id,
					Name = d.Name,
					UrlSlug = d.UrlSlug,
				}).ToPagedListAsync(pagingParams, cancellationToken);
		}
	}
}

