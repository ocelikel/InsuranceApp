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
    public class GetUserOffersService : IGetUserOffersService
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IMapper mapper;

        public GetUserOffersService(IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            this.queryDispatcher = queryDispatcher;
            this.mapper= mapper;
        }
        public async Task<List<OfferViewModel>> GetUserOffersByIdentityNumberAsync(string identityNumber)
        {
            try
            {
                return await queryDispatcher.ExecuteAsync<GetUserOffersByIdentityNumberQuery, List<OfferViewModel>>(new GetUserOffersByIdentityNumberQuery { IdentityNumber=identityNumber});
            }
            catch (Exception ex)
            {

                return default(List<OfferViewModel>);
            }
        }

        public async Task<List<OfferViewModel>> GetUserOffersByProcessIdAsync(Guid processId)
        {
            try
            {
                return await queryDispatcher.ExecuteAsync<GetUserOffersByProcessIdQuery, List<OfferViewModel>>(new GetUserOffersByProcessIdQuery {ProcessId= processId });
            }
            catch (Exception ex)
            {

                return default(List<OfferViewModel>);
            }
        }

    }
}
