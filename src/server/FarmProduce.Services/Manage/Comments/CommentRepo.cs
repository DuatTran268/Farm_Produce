using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
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


		public async Task<IPagedList<CommentItem>> GetFilterComment(IPagingParams pagingParams, string name = null, bool? status = null, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Comment>()
				.AsNoTracking()
				.WhereIf(!string.IsNullOrWhiteSpace(name),
				x => x.Name.Contains(name))
				.WhereIf(status != null, x => x.Status == status)
				.Select(c => new CommentItem()
				{
					Id = c.Id,
					Name = c.Name,
					Rating = c.Rating,
					Created = c.Created,
					CommentText = c.CommentText,
					Status = c.Status

				}).ToPagedListAsync(pagingParams, cancellationToken);

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

		public async Task<bool> DeleteComment(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Comments.Where(t => t.Id == id)
			.ExecuteDeleteAsync(cancellationToken) > 0;
		}
	}
}
