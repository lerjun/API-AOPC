using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class QrCodeLogsModel
    {

        [Key]
        public int Id { get; set; }


        [Column(TypeName = "int")]
        public int? BusinessTypeID { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? EmployeeID { get; set; }


        [Column(TypeName = "Varchar(max)")]
        public string? Longtitude { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Latitude { get; set; }


        [Column(TypeName = "Varchar(max)")]
        public string? IPAddres { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Region { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Country { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? City { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? AreaCode { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? ZipCode { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? MetroCode { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? TimeZone { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? PostalCode { get; set; }

    }
}
