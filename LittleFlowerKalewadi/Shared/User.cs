using System;
using System.Collections.Generic;

namespace LittleFlowerKalewadi.Shared
{
    public partial class User
    {
        public User()
        {

        }

        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public short RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}