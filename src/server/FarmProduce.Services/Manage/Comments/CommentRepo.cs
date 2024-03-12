using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Comments
{
	public class CommentRepo : ICommentRepo
	{
		private readonly FarmDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public CommentRepo(FarmDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}


		public async Task<IList<T>> GetAllComments<T>(Func<IQueryable<Comment>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
		{
			IQueryable<Comment> comments = _context.Set<Comment>().OrderBy(a => a.Name);
			return await mapper(comments).ToListAsync(cancellationToken);
		}
	}
}
