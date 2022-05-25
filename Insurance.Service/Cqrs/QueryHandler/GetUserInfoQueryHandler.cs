using AutoMapper;
using Insurance.Model.Models;
using Insurance.Repository.Repository.Interface;
using Insurance.Service.Cqrs.Query;
using Insurance.Service.Dispatcher;
using Insurance.Service.Extensions;
using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Cqrs.QueryHandler
{
    public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, UserInformationRequest>
    {
        private readonly IRepository<UserInsurance, Guid> userRepository;

        private readonly IMapper mapper;

        public GetUserInfoQueryHandler(IRepository<UserInsurance, Guid> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public UserInformationRequest Execute(GetUserInfoQuery query)
        {
            if (query == default(GetUserInfoQuery))
            {
                throw new CustomException($"{nameof(query)} must not be null");
            }

            if (string.IsNullOrWhiteSpace(query.IdentityNumber))
            {
                throw new CustomException($"{nameof(query.IdentityNumber)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(query.Plate))
            {
                throw new CustomException($"{nameof(query.Plate)} cannot be null");
            }

            return (from user in userRepository.All()
                    where user.IdentityNumber == query.IdentityNumber && user.Plate == query.Plate
                    select new
                    UserInformationRequest
                    {
                        IdentityNumber = user.IdentityNumber,
                        LicenseSerialCode = user.LicenseSerialCode,
                        LicenseSerialNo = user.LicenseSerialNo,
                        Plate = user.Plate
                    }).FirstOrDefault();

        }

        public async Task<UserInformationRequest> ExecuteAsync(GetUserInfoQuery query)
        => await Task.Run(() => Execute(query));
    }
}
