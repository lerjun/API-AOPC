using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class RegistrationOTPModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? OTP { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Email { get; set; }


        [Column(TypeName = "int")]
        public int? Status { get; set; }
    }
}
