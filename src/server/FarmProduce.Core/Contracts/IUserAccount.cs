﻿using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FarmProduce.Core.DTO.ServiceResponses;

namespace FarmProduce.Core.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
        Task<IEnumerable<ApplicationUser>> GetAllAccounts();
        //Task<IEnumerable<UserWithRolesDTO>> GetAllAccountsWithRoles();
        Task<GeneralResponse> CreateAccountByAdmin(UserDTO userDTO);
        Task<IEnumerable<DetailUserDTO>> GetAllUser();
        Task<DetailUserDTO> GetUserWithOrdersById(string userId);
        Task<GeneralResponse> UpdateUserAndOrders(string userId, UserWithOrderDTO detailUserDTO, List<OrderDTO> orderDTOs);
		Task<int> CountTotalUserAccount(CancellationToken cancellationToken = default);
		Task<IPagedList<DetailUserDTO>> GetAllAccountPagination(IPagingParams pagingParams, string name = null, CancellationToken cancellationToken = default);
	}
}
