using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class BusinesslocVM
    {
        public int Id { get; set; }

        public int NoOfVisit { get; set; }


        public string Fullname { get; set; }

  
        public string Businessname { get; set; }


        public string Vendorname { get; set; }

 
        public string Corporatename { get; set; }

        public string Privilegename { get; set; }

        public string Country { get; set; }

        public string Businesstype { get; set; }
    }
}
