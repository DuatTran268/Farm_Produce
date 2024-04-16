using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Images
{
    public interface IImageRepo
    {
        Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Image>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Image>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);
        Task<bool> SetImageAsync(string caption, string imageUrl, CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateImage(Image image, CancellationToken cancellationToken = default);
        Task<Image> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> IsSlugImageExisted(int id, CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateImages(List<Image> images, CancellationToken cancellationToken = default);
<<<<<<< HEAD
		Task<bool> DeleteImage(int id, CancellationToken cancellationToken = default);

	}
=======
        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken=default);
    }
>>>>>>> 85fab6d3716a99a74ca168a9065e65f50aee94c7
}