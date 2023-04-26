using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class MembershipModelVM
    {



        public int Id { get; set; }

        public string MembershipName { get; set; }

        public string? Description { get; set; }

        public string? MembershipID { get; set; }

        public string? UserCount { get; set; }

        public string? VIPCount { get; set; }

        public string? DateCreated { get; set; }



    }
}
