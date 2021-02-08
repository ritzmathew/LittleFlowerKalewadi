using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LittleFlowerKalewadi.Shared;

namespace LittleFlowerKalewadi.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
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
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(RegisterViewModel RegisterViewModel)
        {
            return new User
            {
                EmailAddress = RegisterViewModel.EmailAddress,
                Password = RegisterViewModel.Password
            };
        }
    }
}