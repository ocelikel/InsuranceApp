using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueue
{
    public class GetOfferProcessState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
       
        public string CurrentState { get; set; }

        public DateTime Updated { get; set; }
    }
}
