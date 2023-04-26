using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class VendorModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string VendorName { get; set; }

        [Column(TypeName = "int")]
        public int? BusinessTypeId { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Services { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? WebsiteUrl { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? FeatureImg { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Gallery { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? VendorID{ get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Cno { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Email { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? VideoUrl { get; set; } 
        
        [Column(TypeName = "varchar(MAX)")]
        public string? VrUrl { get; set; }        
        
        [Column(TypeName = "varchar(MAX)")]
        public string? BusinessLocationID { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? Address { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? VendorLogo { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? FileUrl { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? Map { get; set; }



        [Column(TypeName = "int")]
        public int? Status { get; set; }
    }
}
