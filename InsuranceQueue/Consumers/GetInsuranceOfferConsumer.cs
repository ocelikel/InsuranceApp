using Insurance.Service.Services;
using Insurance.Shared.SignalR;
using InsuranceQueueModel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueue.Consumers
{
    public class GetInsuranceOfferConsumer : IConsumer<IProcessRequestAccepted>
    {
        private readonly IHubContext<OfferHub> offerHubContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<GetInsuranceOfferConsumer> _logger;
        private readonly ICreateUserOfferService createofferService;
        private const string basePath= "http://host.docker.internal:8001/api/offer";
        public GetInsuranceOfferConsumer(IPublishEndpoint publishEndpoint,
                                 ILogger<GetInsuranceOfferConsumer> logger,
                                 ICreateUserOfferService createofferService,
                                 IHubContext<OfferHub> offerHubContext)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            this.createofferService = createofferService;
            this.offerHubContext = offerHubContext;
        }

        public async Task Consume(ConsumeContext<IProcessRequestAccepted> context)
        {
            _logger.LogInformation($"{nameof(GetInsuranceOfferConsumer)}: Starting to get insurance offer process for process id = {context.Message.ProcessId}. UTC: {DateTime.UtcNow}.");

            await Task.Delay(15000);
            var offerModelList = await RequestService.GetOffersAsync(basePath, context.Message.Plate, context.Message.IdentityNumber, context.Message.LicenseSerialCode, context.Message.LicenseSerialNo);

            foreach (var offer in offerModelList)
            {
                offer.ProcessId = context.Message.ProcessId;
                offer.OfferDate = DateTime.Now.Date;
                offer.IdentityNumber=context.Message.IdentityNumber;
            }

            await createofferService.ExecuteAsync(offerModelList);

            var json = JsonConvert.SerializeObject(offerModelList);

            await offerHubContext.Clients.All.SendAsync("SEND_OFFER", json);

            await _publishEndpoint.Publish<IProcessCompleted>(new
            {
                ProcessId = context.Message.ProcessId,
                TimeStamp = DateTime.UtcNow
            });

            _logger.LogInformation($"{nameof(GetInsuranceOfferConsumer)}: Completed to get insurance offer process for process id = {context.Message.ProcessId}. UTC: {DateTime.UtcNow}.");
        }
    }
}
