using Insurance.Service.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider provider;

        public EventDispatcher(IServiceProvider provider) => this.provider = provider;

      
        public void Raise<T>(T domainCommand) where T : IDomainCommand
            => provider.GetServices(typeof(IDomainCommandHandler<T>))?.ForEach(x => ((IDomainCommandHandler<T>)x).Handle(domainCommand));

       
        public async Task RaiseAsync<T>(T domainCommand) where T : IDomainCommand
        {
            var tasks = provider.GetServices(typeof(IDomainCommandAsyncHandler<T>))?.Select(x => ((IDomainCommandAsyncHandler<T>)x).HandleAsync(domainCommand));
            await Task.WhenAll(tasks);
        }
    }
}
