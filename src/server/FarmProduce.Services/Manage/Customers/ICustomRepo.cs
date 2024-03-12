using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Services.Manage.Customers
{
    public interface ICustomRepo
    {
        Task<IList<Customer>> GetAllAsync(CustomerQuery query, CancellationToken cancellationToken = default);
        Task<IPagedList<Customer>> GetAllPageAsync(CustomerQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default);
    }
}
