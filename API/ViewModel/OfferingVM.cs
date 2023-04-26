using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class OfferingVM
    {
     
        public int Id { get; set; }

        public string? VendorName { get; set; }
        public string? VendorID { get; set; }

        public string? MembershipName { get; set; }
        public string? MembershipID { get; set; }


        public string? BusinessTypeName { get; set; }
        public string? BusinessTypeID { get; set; }
        public string? PrivilegeID { get; set; }


        public string? OfferingName { get; set; }


        public string? OfferingID { get; set; }

        public string? PromoDesc { get; set; }


        public string? PromoReleaseText { get; set; }


        public string? DateCreated { get; set; }


        public string? ImgUrl { get; set; }


        public string? Status { get; set; }
        public string? URL { get; set; }
        public string? Offerdays { get; set; }
        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
    }
}
