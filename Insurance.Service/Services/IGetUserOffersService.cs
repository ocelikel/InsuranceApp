using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Services
{
    public interface IGetUserOffersService
    {
        Task<List<OfferViewModel>> GetUserOffersByIdentityNumberAsync(string identityNumber);
        Task<List<OfferViewModel>> GetUserOffersByProcessIdAsync(Guid processId);
    }
}
