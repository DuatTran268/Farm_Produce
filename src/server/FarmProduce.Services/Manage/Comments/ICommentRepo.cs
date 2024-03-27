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
		Task<IPagedList<CommentItem>> GetFilterComment(
			IPagingParams pagingParams,
			string name = null,
			bool? status = null,
			CancellationToken cancellationToken = default);


		Task<Comment> GetCommnetByID(int id, CancellationToken cancellationToken = default);


		Task<bool> AddOrUpdateComment(Comment comment, CancellationToken cancellationToken = default);

		Task<bool> DeleteComment(int id, CancellationToken cancellationToken = default);

	}
}
