using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Model.Models
{
    public class Offer: IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
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
