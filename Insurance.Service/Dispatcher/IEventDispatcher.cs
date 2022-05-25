using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public interface IEventDispatcher
    {
        void Raise<T>(T domainEvent) where T : IDomainCommand;
      
        Task RaiseAsync<T>(T domainEvent) where T : IDomainCommand;
    }
}
