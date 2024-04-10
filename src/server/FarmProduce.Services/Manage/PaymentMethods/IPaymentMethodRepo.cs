using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.PaymentMethods
{
    public interface IPaymentMethodRepo
    {
        Task<IPagedList<T>> GetAllPaymentMethod<T>(Func<IQueryable<PaymentMethod>, IQueryable<T>> mapper, IPagingParams pagingParams, CancellationToken cancellationToken = default);

        Task<PaymentMethod> GetPaymentMethodById(int id, CancellationToken cancellationToken = default);
        Task<bool> IsIdExisted(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteWithIDAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddOrUpdateProduct(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    }
}
