using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQueueModel
{
    public interface IProcessRequestAccepted
    {
        Guid ProcessId { get; }
        string Plate { get; }
        string IdentityNumber { get; }
        string LicenseSerialCode { get; }
        string LicenseSerialNo { get; }
    }
}
