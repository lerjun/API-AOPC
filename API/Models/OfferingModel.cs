using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class OfferingModel
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public int? VendorID { get; set; }

        [Column(TypeName = "int")]
        public int? MembershipID { get; set; }

        [Column(TypeName = "int")]
        public int? BusinessTypeID { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? OfferingName { get; set; }


        [Column(TypeName = "Varchar(max)")]
        public string? PromoDesc { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? PromoReleaseText { get; set; }


        [Column(TypeName = "Varchar(max)")]
        public string? ImgUrl { get; set; }

        [Column(TypeName = "int")]
        public int? StatusID { get; set; }

    }
}
