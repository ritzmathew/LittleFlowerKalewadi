using System.Threading.Tasks;

namespace LittleFlowerKalewadi.ViewModels
{
    public interface IRegisterViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public Task RegisterUser();
    }
}