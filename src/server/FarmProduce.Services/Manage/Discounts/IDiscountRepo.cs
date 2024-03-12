using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Discounts
{
	public interface IDiscountRepo
	{
		Task<IList<T>> GetAllDiscount<T>(Func<IQueryable<Discount>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
	}
}
