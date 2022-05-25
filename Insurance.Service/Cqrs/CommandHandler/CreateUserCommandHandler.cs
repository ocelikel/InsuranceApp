using AutoMapper;
using Insurance.Model.Models;
using Insurance.Repository.Repository.Interface;
using Insurance.Service.Cqrs.Command;
using Insurance.Service.Dispatcher;
using Insurance.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Cqrs.CommandHandler
{
    public class CreateUserCommandHandler : IDomainCommandAsyncHandler<CreateUserCommand>
    {
        private readonly IRepository<UserInsurance, Guid> userInsuranceRepository;

        private readonly IMapper mapper;

        public CreateUserCommandHandler(IRepository<UserInsurance, Guid> userInsuranceRepository, IMapper mapper)
        {
            this.userInsuranceRepository = userInsuranceRepository;
            this.mapper = mapper;
        }

        public async Task HandleAsync(CreateUserCommand domainCommand)
        {
            if (domainCommand == default(CreateUserCommand))
            {
                throw new CustomException($"{nameof(domainCommand)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(domainCommand.Plate))
            {
                throw new CustomException($"{nameof(domainCommand.Plate)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(domainCommand.IdentityNumber))
            {
                throw new CustomException($"{nameof(domainCommand.IdentityNumber)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(domainCommand.LicenseSerialCode))
            {
                throw new CustomException($"{nameof(domainCommand.LicenseSerialCode)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(domainCommand.LicenseSerialNo))
            {
                throw new CustomException($"{nameof(domainCommand.LicenseSerialNo)} cannot be null");
            }

            await userInsuranceRepository.AddAsync(mapper.Map<UserInsurance>(domainCommand));
        }
    }
}
