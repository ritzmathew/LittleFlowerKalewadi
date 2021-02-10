using System.Threading.Tasks;

namespace LittleFlowerKalewadi.ViewModels
{
    public interface ILoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public Task<bool> LoginUser();
    }
}