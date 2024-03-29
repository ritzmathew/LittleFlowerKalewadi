using System;
using System.Threading.Tasks;

namespace LittleFlowerKalewadi.ViewModels
{
    public interface IRegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public Task RegisterUser();
    }
}