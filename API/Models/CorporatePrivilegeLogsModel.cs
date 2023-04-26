using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class CorporatePrivilegeLogsModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string RedeemedBy { get; set; }

  

        [Column(TypeName = "int")]
        public int? PrivilegeOwnerId { get; set; }
        [Column(TypeName = "int")]
        public int? BusinessId { get; set; }       
        [Column(TypeName = "int")]
        public int? VendorId { get; set; }        
        [Column(TypeName = "int")]
        public int? PrivilegeId { get; set; } 
        
        [Column(TypeName = "datetime")]
        public DateTime? DateRedeemed { get; set; }   
        
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }

    }
}
