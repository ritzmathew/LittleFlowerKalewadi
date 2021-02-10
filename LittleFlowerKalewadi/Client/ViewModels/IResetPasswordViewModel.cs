using System.Threading.Tasks;

namespace LittleFlowerKalewadi.ViewModels
{
    public interface IResetPasswordViewModel
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Task ResetPassword();
    }
}