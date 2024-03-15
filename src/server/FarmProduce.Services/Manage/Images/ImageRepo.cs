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

        public async Task<IPagedList<T>> GetAllPageAsync<T>(Func<IQueryable<Image>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<Image> images = _context.Set<Image>();
            return await mapper(images).ToPagedListAsync(pagingParams, cancellationToken);
        }
    }
}
