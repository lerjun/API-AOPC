using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class PrivilegeVM
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Validity { get; set; }

        public int? noExpiry { get; set; }

        public string? ImgUrl { get; set; }

        public string? PrivilegeID { get; set; }

        public int? isVIP { get; set; }

        public string? FeatureImg { get; set; }
        
        public string? TMC { get; set; }

        public string? VendorID { get; set; }

        public string? VendorName { get; set; }

        public string? BusinessTypeName { get; set; }

        public string? BusinessTypeID { get; set; }

        public string? Status { get; set; }

        public string? DateCreated { get; set; }

        public string? Mechanics { get; set; }
        public string? Active { get; set; }


    }
}
