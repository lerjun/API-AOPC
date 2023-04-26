using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class UsersModel
    {
        [Key]
        public int? Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Username { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Password { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Fullname { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Fname { get; set; }

        [Column(TypeName = "varchar(max)")]

        public string? Lname { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Mname { get; set; }        
        [Column(TypeName = "varchar(max)")]
        public string? Address { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Gender { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? EmployeeID { get; set; }

        [Column(TypeName = "int")]
        public int? CorporateID { get; set; }

        [Column(TypeName = "int")]
        public int? PositionID { get; set; }
        //
        [Column(TypeName = "varchar(max)")]
        public string? JWToken { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? FilePath { get; set; }  
        
        [Column(TypeName = "varchar(255)")]
        public string? OTP { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Cno { get; set; }
        [Column(TypeName = "int")]
        public int? Type { get; set; }

        [Column(TypeName = "int")]
        public int? Active { get; set; }
        [Column(TypeName = "int")]
        public int? isVIP { get; set; }

    }
}
