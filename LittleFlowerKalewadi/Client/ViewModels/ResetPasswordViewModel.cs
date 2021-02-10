using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LittleFlowerKalewadi.Shared;
using System.ComponentModel.DataAnnotations;

namespace LittleFlowerKalewadi.ViewModels
{
    public class ResetPasswordViewModel : IResetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        private HttpClient _httpClient;
        public ResetPasswordViewModel()
        {

        }
        public ResetPasswordViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ResetPassword()
        {
            await _httpClient.PostAsJsonAsync<User>("user/resetpassword", this);
        }

        public static implicit operator ResetPasswordViewModel(User user)
        {
            return new ResetPasswordViewModel
            {
                Password = user.Password
            };
        }

        public static implicit operator User(ResetPasswordViewModel resetPasswordViewModel)
        {
            return new User
            {
                Password = resetPasswordViewModel.Password
            };
        }
    }
}