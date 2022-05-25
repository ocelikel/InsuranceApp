using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Shared.ViewModels
{
    public class UserInformationRequest
    {
        public string Plate { get; set; }
        public string IdentityNumber { get; set; }
        public string LicenseSerialCode { get; set; }
        public string LicenseSerialNo { get; set; }
    }
}
