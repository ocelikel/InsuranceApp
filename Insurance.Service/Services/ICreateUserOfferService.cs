using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Services
{
    public interface ICreateUserOfferService
    {
        Task ExecuteAsync(List<OfferViewModel> offerViewModelList);
    }
}
