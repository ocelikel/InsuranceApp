using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueue
{
    public class GetOfferProcessStateMachineDefinition : SagaDefinition<GetOfferProcessState>
    {
        public GetOfferProcessStateMachineDefinition()
        {
           
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<GetOfferProcessState> sagaConfigurator)
        {
          
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 5000, 10000));

            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}
