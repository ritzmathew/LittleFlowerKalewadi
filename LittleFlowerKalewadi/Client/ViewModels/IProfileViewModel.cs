using System;
using System.Threading.Tasks;

namespace LittleFlowerKalewadi.ViewModels
{
    public interface IProfileViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public Task ChangePassword();
        public Task GetProfile();
        public Task UpdateProfile();
    }
}