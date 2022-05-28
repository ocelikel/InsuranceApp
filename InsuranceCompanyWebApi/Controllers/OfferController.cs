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
                new OfferResponseModel(){CompanyLogo="Company1Logo",CompanyName="ABC Company",Description="Company1 Description1",Plate="34GS1905",Price=950},
                new OfferResponseModel(){CompanyLogo="Company2Logo",CompanyName="XYZ Company",Description="Company2 Description2",Plate="34FB1907",Price=1050},
                new OfferResponseModel(){CompanyLogo="Company3Logo",CompanyName="QWE Company",Description="Company3 Description3",Plate="34BJK1903",Price=850},
                new OfferResponseModel(){CompanyLogo="Company4Logo",CompanyName="ASD Company",Description="Company4 Description5",Plate="61TS1967",Price=1250}

            };

            return Ok(offerList);
        }
    }
}
