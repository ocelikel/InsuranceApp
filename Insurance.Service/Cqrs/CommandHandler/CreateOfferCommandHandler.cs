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
    public class CreateOfferCommandHandler : IDomainCommandAsyncHandler<CreateOfferCommand>
    {
        private readonly IRepository<Offer, Guid> offerRepository;

        private readonly IMapper mapper;

        public CreateOfferCommandHandler(IRepository<Offer, Guid> offerRepository, IMapper mapper)
        {
            this.offerRepository = offerRepository;
            this.mapper = mapper;
        }

        public async Task HandleAsync(CreateOfferCommand domainCommand)
        {
            if (domainCommand == default(CreateOfferCommand))
            {
                throw new CustomException($"{nameof(domainCommand)} cannot be null");
            }

            await offerRepository.AddAsync(mapper.Map<Offer>(domainCommand));
        }
    }
}
