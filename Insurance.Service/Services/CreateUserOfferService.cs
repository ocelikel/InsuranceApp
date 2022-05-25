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
    public class CreateUserOfferService : ICreateUserOfferService
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly IMapper mapper;

        public CreateUserOfferService(IEventDispatcher eventDispatcher, IMapper mapper)
        {
            this.eventDispatcher = eventDispatcher;
            this.mapper = mapper;
        }

        public async Task ExecuteAsync(List<OfferViewModel> offerViewModelList)
        {
            try
            {
                foreach (var offer in offerViewModelList)
                {
                    await eventDispatcher.RaiseAsync<CreateOfferCommand>(mapper.Map<CreateOfferCommand>(offer));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
