using Insurance.Service.Cqrs.Command;
using Insurance.Service.Dispatcher;
using Insurance.Service.Services;
using Insurance.Shared.ViewModels;
using InsuranceQueueModel;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Insurance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly ICreateUserInsuranceService createInsuranceService;
        private readonly IGetUserOffersService getUserOffersService;
        private readonly IUserService userService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<ICheckProcess> _checkProcessClient;
        //private readonly IEventDispatcher eventDispatcher;

        public InsuranceController(ICreateUserInsuranceService createInsuranceService,
            ICreateUserOfferService createOfferService,
            IGetUserOffersService getUserOffersService,
            IUserService userService, 
            IPublishEndpoint _publishEndpoint,
            IRequestClient<ICheckProcess> _checkProcessClient)
        {
            this.createInsuranceService = createInsuranceService;
            this.getUserOffersService = getUserOffersService;
            this.userService = userService;
            this._publishEndpoint = _publishEndpoint;
            this._checkProcessClient = _checkProcessClient;
        }

        [HttpPost("Offer")]
        public async Task<IActionResult> PostOffer(UserInformationRequest offerRequest)
        {
            try
            {
                var userExists = await userService.CheckUserExistsAsync(offerRequest.IdentityNumber, offerRequest.Plate);
               
                if (!userExists)
                {
                    await createInsuranceService.ExecuteAsync(offerRequest);
                }

                Guid processId = Guid.NewGuid();

                OfferProcessResponse response = new OfferProcessResponse()
                {
                    ProcessId = processId
                };

                await _publishEndpoint.Publish<IProcessRequestAccepted>(new
                {
                    ProcessId = processId,
                    Plate = offerRequest.Plate,
                    IdentityNumber = offerRequest.IdentityNumber,
                    LicenseSerialCode = offerRequest.LicenseSerialCode,
                    LicenseSerialNo = offerRequest.LicenseSerialNo
                });

                return Accepted(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("Offers/{identityNumber}")]
        public async Task<IActionResult> GetOffers(string identityNumber)
        {
            try
            {
                var offers = await getUserOffersService.GetUserOffersByIdentityNumberAsync(identityNumber);

                return Ok(JsonConvert.SerializeObject(offers));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Offer/Process/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetProcessStatus(Guid id)
        {
            try
            {
                var (status, notFound) = await _checkProcessClient.GetResponse<IProcessStatus, IProcessNotFound>(new
                {
                    ProcessId = id
                });

                if (status.IsCompletedSuccessfully)
                {
                    var response = await status;
                    return Ok(response.Message);
                }

                var notFoundResponse = await notFound;
                return NotFound(notFoundResponse.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Offer/{processid}")]
        public async Task<IActionResult> GetOffers(Guid processid)
        {
            try
            {
                var offers = await getUserOffersService.GetUserOffersByProcessIdAsync(processid);
                return Ok(offers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetUserInfo([FromQuery]UserInfoViewModel userInfo)
        {
            try
            {
                var user = await userService.GetUserInfoAsync(userInfo);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
