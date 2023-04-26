using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class BusinessCardVM
    {


        public string? Description { get; set; }

        public string? FeatureImg { get; set; }

        public string? Status { get; set; }

        public string? Location { get; set; }

        public string? BusinessID { get; set; }

        public string? Country { get; set; }

        public string? Cno { get; set; }

        public string? Email { get; set; }

        public string? Url { get; set; }

        public string? Gallery { get; set; }

        public string? FilePath { get; set; }

        public string? HotelName { get; set; }

        public string? Map { get; set; }

      
    
    }
}
