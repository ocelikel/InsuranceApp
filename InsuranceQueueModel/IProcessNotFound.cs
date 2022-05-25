using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueueModel
{
    public interface IProcessNotFound
    {
        Guid ProcessId { get; }
    }
}
