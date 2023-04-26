using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class PositionModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? PositionName { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }   
        
        [Column(TypeName = "varchar(MAX)")]
        public string? PositionID { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string? DateCreated { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Status { get; set; }

    }
}
