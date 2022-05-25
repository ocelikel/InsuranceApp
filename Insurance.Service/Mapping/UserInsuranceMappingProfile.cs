using AutoMapper;
using Insurance.Model.Models;
using Insurance.Service.Cqrs.Command;
using Insurance.Service.Cqrs.Query;
using Insurance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Mapping
{
    public class UserInsuranceMappingProfile : Profile
    {
        public UserInsuranceMappingProfile()
        {
            CreateMap<CreateUserCommand, UserInsurance>().ReverseMap();
            CreateMap<CreateUserCommand, UserInformationRequest>().ReverseMap();
            CreateMap<UserOfferQueryRequest, GetUserOffersByIdentityNumberQuery>().ReverseMap();
            CreateMap<Offer, CreateOfferCommand>().ReverseMap();
            CreateMap<OfferViewModel, CreateOfferCommand>().ReverseMap();
               
        }
    }
}
