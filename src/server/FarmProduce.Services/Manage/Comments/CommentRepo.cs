using FarmProduce.Core.Contracts;
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

		public async Task<IPagedList<T>> GetAllComments<T>(Func<IQueryable<Comment>, IQueryable<T>> mapper,IPagingParams pagingParams ,CancellationToken cancellationToken = default)
		{
			IQueryable<Comment> comments = _context.Set<Comment>().OrderBy(a => a.Name);
			return await mapper(comments).ToPagedListAsync(pagingParams, cancellationToken);
		}

		public async Task<Comment> GetCommnetByID(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Comment>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		}

		public async Task<bool> AddOrUpdateComment(Comment comment, CancellationToken cancellationToken = default)
		{
			if (comment.Id > 0)
			{
				_context.Comments.Update(comment);
				_memoryCache.Remove($"comment.by-id.{comment.Id}");
			}
			else
			{
				_context.Comments.Add(comment);
			}
			return await _context.SaveChangesAsync(cancellationToken) > 0;
		}
	}
}
