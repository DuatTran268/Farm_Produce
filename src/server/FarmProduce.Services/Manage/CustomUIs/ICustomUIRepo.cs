using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.CustomUIs
{
    public interface ICustomUIRepo
    {
        Task<IList<T>> GetAllAsync<T>(Func<IQueryable<CustomUI>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<CustomUI>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdate(CustomUI custom, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken);
        Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);


    }
}
