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
		Task<IList<T>> GetAllComments<T>(Func<IQueryable<Comment>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);


		Task<Comment> GetCommnetByID(int id, CancellationToken cancellationToken = default);



	}
}
