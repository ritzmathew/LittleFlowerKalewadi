using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LittleFlowerKalewadi.Shared;
using System.ComponentModel.DataAnnotations;

namespace LittleFlowerKalewadi.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }

        private HttpClient _httpClient;
        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginUser()
        {
            await _httpClient.PostAsJsonAsync<User>("user/loginuser", this);
        }

        public static implicit operator LoginViewModel(User user)
        {
            return new LoginViewModel
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(LoginViewModel loginViewModel)
        {
            return new User
            {
                EmailAddress = loginViewModel.EmailAddress,
                Password = loginViewModel.Password
            };
        }
    }
}