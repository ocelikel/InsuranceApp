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
    public class GetUserOffersByIdentityNumberQueryHandler : IQueryHandler<GetUserOffersByIdentityNumberQuery, List<OfferViewModel>>
    {
        private readonly IRepository<Offer, Guid> offerRepository;

        private readonly IMapper mapper;
        public GetUserOffersByIdentityNumberQueryHandler(IRepository<Offer, Guid> offerRepository, IMapper mapper)
        {
            this.offerRepository= offerRepository;
            this.mapper= mapper;
        }
        public List<OfferViewModel> Execute(GetUserOffersByIdentityNumberQuery query)
        {
            if (query == default(GetUserOffersByIdentityNumberQuery))
            {
                throw new CustomException($"{nameof(query)} must not be null");
            }

            if (string.IsNullOrWhiteSpace(query.IdentityNumber))
            {
                throw new CustomException($"{nameof(query.IdentityNumber)} cannot be null");
            }

            return (from offer in offerRepository.All()
                    where offer.IdentityNumber == query.IdentityNumber
                    select new 
                    OfferViewModel 
                    { 
                        CompanyLogo=offer.CompanyLogo,
                        CompanyName=offer.CompanyName,
                        Description=offer.Description,
                        Plate=offer.Plate,
                        Price=offer.Price,
                        OfferDate=offer.OfferDate

                    }).ToList();
        }

        public async Task<List<OfferViewModel>> ExecuteAsync(GetUserOffersByIdentityNumberQuery query)
        => await Task.Run(() => Execute(query));
    }
}
