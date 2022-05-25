using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public interface IDomainCommandAsyncHandler<in T> where T : IDomainCommand
    {
        Task HandleAsync(T domainCommand);
    }
}
