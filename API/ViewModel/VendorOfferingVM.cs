using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class VendorOfferingVM
    {
        public int Id { get; set; }

        public string VendorName { get; set; }
        public string Description { get; set; }
        public string Services { get; set; }
        public string WebsiteUrl { get; set; }
        public string FeatureImg { get; set; }
        public string Gallery { get; set; }
        public string Cno { get; set; }
        public string Email { get; set; }
        public string VideoUrl { get; set; }
        public string VrUrl { get; set; }
        public string OfferingCategoryName { get; set; }
        public string OfferingDesc { get; set; }
        public string PromoDesc { get; set; }
        public string Expiry { get; set; }
        public string MembershipName { get; set; }
        public string DateEnded { get; set; }
        public string DateUsed { get; set; }
        public string OfferingCategoryDesc { get; set; }
        public string PromoReleaseText { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string BusinessTypeName { get; set; }
        public string BusinessTypeDesc { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDesc { get; set; }
        public string Address { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessCno { get; set; }
        public string Url { get; set; }
        public string BusinessServ { get; set; }
        public string ImageUrl { get; set; }
        public string BusinessGallery { get; set; }
        public string Location { get; set; }
        public string FileUrl { get; set; }
        public string Map { get; set; }
        public string VendorLogo { get; set; }


    }
}
