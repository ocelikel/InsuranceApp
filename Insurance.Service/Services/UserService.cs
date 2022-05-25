using AutoMapper;
using Insurance.Service.Cqrs.Query;
using Insurance.Service.Dispatcher;
using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IMapper mapper;

        public UserService(IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
        }

        public async Task<UserInformationRequest> GetUserInfoAsync(UserInfoViewModel userInfo)
        {
            try
            {
                return await queryDispatcher.ExecuteAsync<GetUserInfoQuery, UserInformationRequest>(new GetUserInfoQuery { IdentityNumber = userInfo.IdentityNumber,Plate=userInfo.Plate });
            }
            catch (Exception ex)
            {
                return default(UserInformationRequest);
            }
        }

        public async Task<bool> CheckUserExistsAsync(string identityNumber, string plate)
        {
            try
            {
                var user = await queryDispatcher.ExecuteAsync<GetUserInfoQuery, UserInformationRequest>(new GetUserInfoQuery { IdentityNumber = identityNumber, Plate = plate });

                if (user == default(UserInformationRequest))
                {
                    return default(bool);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
