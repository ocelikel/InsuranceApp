using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceCompanyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOffers([FromQuery] OfferInput input)
        {
            var offerList = new List<OfferResponseModel>()
            {
                new OfferResponseModel(){CompanyLogo="Company1Logo",CompanyName="Company1",Description="Company1 Description",Plate="plate",Price=950},
                new OfferResponseModel(){CompanyLogo="Company2Logo",CompanyName="Company2",Description="Company2 Description",Plate="plate",Price=1050},
                new OfferResponseModel(){CompanyLogo="Company3Logo",CompanyName="Company3",Description="Company3 Description",Plate="plate",Price=850},
                new OfferResponseModel(){CompanyLogo="Company4Logo",CompanyName="Company4",Description="Company4 Description",Plate="plate",Price=1250}

            };

            return Ok(offerList);
        }
    }
}
