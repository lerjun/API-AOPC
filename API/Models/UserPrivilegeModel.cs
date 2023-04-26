using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class UserPrivilegeModel
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public int? PrivilegeId { get; set; }        
        
        [Column(TypeName = "int")]
        public int? UserID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Validity { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        //
        [Column(TypeName = "varchar(MAX)")]
        public string ImgUrl { get; set; }


    }
}
