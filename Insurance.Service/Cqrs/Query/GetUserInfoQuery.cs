using Insurance.Service.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Cqrs.Query
{
    public class GetUserInfoQuery:IQuery
    {
        public string IdentityNumber { get; set; }
        public string Plate { get; set; }
    }
}
