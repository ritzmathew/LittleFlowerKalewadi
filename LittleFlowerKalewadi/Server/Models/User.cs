using System;
using System.Collections.Generic;

namespace LittleFlowerKalewadi.Server.Models
{
    public partial class User
    {
        public User()
        {

        }

        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Staff { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}