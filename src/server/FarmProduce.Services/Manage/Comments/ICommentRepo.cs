using FarmProduce.Core.Contracts;
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
		Task<IPagedList<T>> GetAllComments<T>(Func<IQueryable<Comment>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);


		Task<Comment> GetCommnetByID(int id, CancellationToken cancellationToken = default);



	}
}
