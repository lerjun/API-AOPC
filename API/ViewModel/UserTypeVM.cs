using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class UserTypeVM
    {

        public string Id { get; set; }


        public string UserType { get; set; }

        public string Description { get; set; }


        public string Status { get; set; }

    }
}
