using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class MembershipModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime? DateUsed { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime? DateEnded { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "int")]
        public int? Status { get; set; }
    }
}
