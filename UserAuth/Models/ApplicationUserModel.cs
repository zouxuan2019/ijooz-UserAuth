using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuth.Models
{

    [NotMapped]
    public class ApplicationUserModel
    {

        
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Discriminator { get; set; }

        public string Email { get; set; }


    }
}
