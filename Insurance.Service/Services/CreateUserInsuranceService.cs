using AutoMapper;
using Insurance.Service.Cqrs.Command;
using Insurance.Service.Dispatcher;
using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Services
{
    public class CreateUserInsuranceService : ICreateUserInsuranceService
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly IMapper mapper;
        public CreateUserInsuranceService(IEventDispatcher eventDispatcher, IMapper mapper)
        {
            this.eventDispatcher = eventDispatcher;
            this.mapper = mapper;
        }
        public async Task ExecuteAsync(UserInformationRequest getOfferRequest)
        {
            try
            {
                await eventDispatcher.RaiseAsync<CreateUserCommand>(mapper.Map<CreateUserCommand>(getOfferRequest));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
