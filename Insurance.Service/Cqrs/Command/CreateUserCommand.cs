using Insurance.Service.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Cqrs.Command
{
    public class CreateUserCommand : IDomainCommand
    {
        public string Plate { get; set; }
        public string IdentityNumber { get; set; }
        public string LicenseSerialCode { get; set; }
        public string LicenseSerialNo { get; set; }
    }
}
