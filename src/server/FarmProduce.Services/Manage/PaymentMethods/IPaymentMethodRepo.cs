using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.PaymentMethods
{
	public interface IPaymentMethodRepo
	{
		Task<IList<T>> GetAllPaymentMethod<T>(Func<IQueryable<PaymentMethod>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

		Task<PaymentMethod> GetPaymentMethodById(int id, CancellationToken cancellationToken = default);
	
	}
}
