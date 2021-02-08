using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LittleFlowerKalewadi.Shared;

namespace LittleFlowerKalewadi.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        private HttpClient _httpClient;
        public RegisterViewModel()
        {

        }
        public RegisterViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterUser()
        {
            await _httpClient.PutAsJsonAsync<User>("user/registeruser", this);
        }

        public static implicit operator RegisterViewModel(User user)
        {
            return new RegisterViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                CreatedDate = user.CreatedDate
            };
        }

        public static implicit operator User(RegisterViewModel registerViewModel)
        {
            return new User
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                DateOfBirth = registerViewModel.DateOfBirth,
                EmailAddress = registerViewModel.EmailAddress,
                Password = registerViewModel.Password,
                CreatedDate = registerViewModel.CreatedDate
            };
        }
    }
}