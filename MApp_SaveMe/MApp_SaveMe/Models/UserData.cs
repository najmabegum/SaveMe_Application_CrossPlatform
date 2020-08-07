using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class UserData
    {
        public int UserDataID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public int NumberMembersInFamily { get; set; }
        public int NumberKidsInFamily { get; set; }
        public int PassKey { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModiﬁed { get; set; }

    }
}
