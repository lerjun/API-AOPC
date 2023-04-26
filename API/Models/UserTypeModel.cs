using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class UserTypeModel
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string UserType { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Status { get; set; }


    }
}
