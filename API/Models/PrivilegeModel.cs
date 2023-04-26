using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class PrivilegeModel
    {

        [Key]
        public int Id { get; set; }


        [Column(TypeName = "Varchar(max)")]
        public string? Title { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Description { get; set; }      
        
        [Column(TypeName = "Varchar(max)")]
        public string? Mechanics { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Validity { get; set; }

        [Column(TypeName = "int")]
        public int? NoExpiry { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }

    }
}
