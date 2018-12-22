using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ManagementOfUsers.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", LastName, FirstName, Patronymic);
        }
    }
}
