using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class CorporatePrivilegesModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public int? CorporateID { get; set; }     
        
        [Column(TypeName = "int")]
        public int? MembershipID { get; set; }

        public DateTime? DateIssued { get; set; }

        public DateTime? DateExpired { get; set; }

        [Column(TypeName = "int")]
        public int? Count { get; set; }        
        
        [Column(TypeName = "int")]
        public int? Size { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "int")]
        public int? Status { get; set; }
    }
}
