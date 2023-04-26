using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class UserMembershipModel
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public int? UserID { get; set; }        
        
        [Column(TypeName = "int")]
        public int? MembershipID { get; set; }

        [Column(TypeName = "int")]
        public int? MembershipNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Validity { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }

    }
}
