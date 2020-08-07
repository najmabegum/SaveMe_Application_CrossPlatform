using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class UserRegistration
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public int NumberMembersInFamily { get; set; }
        public int NumberKidsInFamily { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
