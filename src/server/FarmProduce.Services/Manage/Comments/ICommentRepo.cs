using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Comments
{
	public interface ICommentRepo
	{
		Task<IPagedList<T>> GetAllComments<T>(Func<IQueryable<Comment>, IQueryable<T>> mapper, CommentQuery commentQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default);
        Task<Comment> GetCommnetByID(int id, CancellationToken cancellationToken = default);
		Task<bool> DeleteWithIdsync(int id, CancellationToken cancellationToken);
		Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
		Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);
		Task<bool> AddOrUpdate(Comment comment, CancellationToken cancellationToken = default);



    }
}
