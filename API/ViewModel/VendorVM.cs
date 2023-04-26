using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class VendorVM
    {

        public string Id { get; set; }


        public string VendorName { get; set; }
        public string BusinessName { get; set; }


        public int? BusinessId { get; set; }

   
        public string? Description { get; set; }


        public string? Services { get; set; }
        public string? BusinessTypeName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }


        public string? WebsiteUrl { get; set; }

        public string? FeatureImg { get; set; }


        public string? Gallery { get; set; }


        public string? Cno { get; set; }


        public string? Email { get; set; }


        public string? VideoUrl { get; set; }


        public string? VrUrl { get; set; }
        public string DateCreated { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string OfferingDesc { get; set; }
        public string PromoDesc { get; set; }
        public string VendorID { get; set; }
        public string FileUrl { get; set; }
        public string Map { get; set; }
        public string VendorLogo { get; set; }
        public string Address { get; set; }
        public string Vendorlocation { get; set; }
        public string BID { get; set; }
        public string BtypeID { get; set; }

    }
}
