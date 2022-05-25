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
    public class GetUserOffersByProcessIdQueryHandler : IQueryHandler<GetUserOffersByProcessIdQuery, List<OfferViewModel>>
    {
        private readonly IRepository<Offer, Guid> offerRepository;

        private readonly IMapper mapper;

        public GetUserOffersByProcessIdQueryHandler(IRepository<Offer, Guid> offerRepository, IMapper mapper)
        {
            this.offerRepository = offerRepository;
            this.mapper = mapper;
        }

        public List<OfferViewModel> Execute(GetUserOffersByProcessIdQuery query)
        {
            if (query == default(GetUserOffersByProcessIdQuery))
            {
                throw new CustomException($"{nameof(query)} must not be null");
            }

            if (query.ProcessId == Guid.Empty)
            {
                throw new CustomException($"{nameof(query.ProcessId)} cannot be null");
            }

            return (from offer in offerRepository.All()
                    where offer.ProcessId == query.ProcessId
                    select new
                    OfferViewModel
                    {
                        IdentityNumber = offer.IdentityNumber,
                        CompanyLogo = offer.CompanyLogo,
                        CompanyName = offer.CompanyName,
                        Description = offer.Description,
                        Plate = offer.Plate,
                        Price = offer.Price

                    }).ToList();

        }

        public async Task<List<OfferViewModel>> ExecuteAsync(GetUserOffersByProcessIdQuery query)
        => await Task.Run(() => Execute(query));
    }
}
