using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class BusinessTypeVM
    {
        public int Id { get; set; }

        public string? BusinessTypeName { get; set; }

        public string? Description { get; set; }

        public string? DateCreated { get; set; }
        public string? status { get; set; }
        public string? BusinessTypeID { get; set; }
        public string? ImgURL { get; set; }
        public string? PromoText { get; set; }
        public string? isVIP { get; set; }

      
    
    }
}
