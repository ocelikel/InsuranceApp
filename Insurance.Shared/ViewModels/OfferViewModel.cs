using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Shared.ViewModels
{
    public class OfferViewModel
    {
        public Guid ProcessId { get; set; }
        public string IdentityNumber { get; set; }
        public string CompanyName { get; set; }
        
        public string CompanyLogo { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Plate { get; set; }
        public DateTime OfferDate { get; set; }
    }
}
