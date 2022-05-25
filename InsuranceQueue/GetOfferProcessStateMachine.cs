using InsuranceQueueModel;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueue
{
    public class GetOfferProcessStateMachine : MassTransitStateMachine<GetOfferProcessState>
    {
        public GetOfferProcessStateMachine()
        {
            Event(() =>ProcessRequestAccepted, x => x.CorrelateById(m => m.Message.ProcessId));
            Event(() => ProcessCompleted, x => x.CorrelateById(m => m.Message.ProcessId));

            Event(() => CheckProcessStatusRequested, x =>
            {
                x.CorrelateById(m => m.Message.ProcessId);
                
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<IProcessNotFound>(new
                        {
                            ProcessId = context.Message.ProcessId
                        });
                    }
                }));
            });

            InstanceState(x => x.CurrentState);
          
            Initially(
                When(ProcessRequestAccepted)
                    .Then(context =>
                    {
                        context.Instance.Updated = DateTime.UtcNow;

                    })
                    .TransitionTo(Accepted)
            );

            DuringAny(
                When(CheckProcessStatusRequested)
                    .RespondAsync(x => x.Init<IProcessStatus>(new
                    {
                        ProcessId = x.Instance.CorrelationId,
                        State = x.Instance.CurrentState
                    }))
            );

            DuringAny(
                When(ProcessCompleted)
                    .TransitionTo(Completed)
            );

        }

      
        public State Accepted { get; private set; }

       
        public State Completed { get; private set; }

       
        public Event<IProcessRequestAccepted> ProcessRequestAccepted { get; private set; }

      
        public Event<IProcessCompleted> ProcessCompleted { get; private set; }

       
        public Event<ICheckProcess> CheckProcessStatusRequested { get; private set; }
    }
}
