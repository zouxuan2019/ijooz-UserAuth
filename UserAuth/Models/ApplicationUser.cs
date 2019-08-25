using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UserAuth.Models
{

  
    public class ApplicationUser:IdentityUser
    {

        [Column(TypeName = "nvarchar(20)")]
        public string Title { get; set; }
        [Column(TypeName="nvarchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Gender { get; set; }
        public string Discriminator { get; set; }


    }
}
