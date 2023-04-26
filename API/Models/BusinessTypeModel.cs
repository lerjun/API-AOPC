using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class BusinessTypeModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? BusinessTypeName { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? ImgURL { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? PromoText { get; set; }

        [Column(TypeName = "int")]
        public int? Status { get; set; }        
        
        [Column(TypeName = "int")]
        public int? isVIP { get; set; }



    }
}
