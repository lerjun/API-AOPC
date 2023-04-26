using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class BusinessModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? BusinessName { get; set; }

        [Column(TypeName = "int")]
        public int? TypeId { get; set; } 
        
        [Column(TypeName = "int")]
        public int? LocationID { get; set; }  
        
        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Address { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Cno { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Email { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Url { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Services { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? FeatureImg { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Gallery { get; set; }

        [Column(TypeName = "int")]
        public int? Active { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? FilePath { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Map { get; set; }

    }
}
