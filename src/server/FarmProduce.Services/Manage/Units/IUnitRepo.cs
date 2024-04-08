using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Units
{
    public interface IUnitRepo
    {
        Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Unit>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Unit>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithSlugAsync(string slug, CancellationToken cancellationToken);

        Task<bool> IsSlugUnitExisted(int id, string urlSlug, CancellationToken cancellationToken = default);

        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);

        Task<bool> AddOrUpdate(Unit unit, CancellationToken cancellationToken = default);
      
    }
}
