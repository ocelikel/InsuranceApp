using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public interface IDomainCommandHandler<in T> where T : IDomainCommand
    {
        void Handle(T domainEvent);
    }
}
