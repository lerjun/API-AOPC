using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class BusinessLocation
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Country { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? City { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? PostalCode { get; set; }
        [Column(TypeName = "int")]
        public int? Active { get; set; }


    }
}
