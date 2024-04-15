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

namespace FarmProduce.Services.Manage.Images
{
    public class ImageRepo:IImageRepo
    {
        private readonly FarmDbContext _context;

        public ImageRepo(FarmDbContext context)
        {
            _context = context;
        }
        public async Task<IList<T>> GetAllAsync<T>(Func<IQueryable<Image>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<Image> images = _context.Set<Image>();
            return await mapper(images).ToListAsync();
        }
        public async Task<Image> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Image>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        }
         public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Image>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<Image> images = _context.Set<Image>();
            return await mapper(images).ToPagedListAsync(pagingParams, cancellationToken);
        }
        public async Task<bool> IsSlugImageExisted(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Image>().AnyAsync(x => x.Id != id );
        }
        public async Task<bool> SetImageAsync(string caption, string imageUrl, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Image>()
                .Where(x => x.Caption == caption)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.UrlImage, imageUrl), cancellationToken) > 0;
        }
        public async Task<bool> AddOrUpdateImage(Image image, CancellationToken cancellationToken = default)
        {
            if (image.Id > 0)
            {
                _context.Update(image);
            }
            else
            {
                _context.Add(image);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddOrUpdateImages(List<Image> images, CancellationToken cancellationToken = default)
        {
            foreach (var image in images)
            {
                if (image.Id > 0)
                {
                    _context.Update(image);
                }
                else
                {
                    _context.Add(image);
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Image>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Image>().Remove(result);
                return true;
            }
        }
        public async Task<bool> IsIdExisted(int id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Image>().AnyAsync(x => x.Id != id);
        }
        public async Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Image>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
            {
                return false;
            }
            else
            {
                _context.Set<Image>().Remove(result);
                return true;
            }
        }

		public async Task<bool> DeleteImage(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Images.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken) > 0;
		}
	}
}
