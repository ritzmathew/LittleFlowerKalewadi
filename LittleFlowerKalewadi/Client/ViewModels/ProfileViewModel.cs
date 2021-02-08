using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LittleFlowerKalewadi.Shared;

namespace LittleFlowerKalewadi.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        private HttpClient _httpClient;
        public ProfileViewModel()
        {

        }
        public ProfileViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetProfile()
        {
            User user = await _httpClient.GetFromJsonAsync<User>($"user/getprofile/{this.UserId}");
            LoadCurrentObject(user);
            this.Message = "Profile loaded successfully";
        }
        public async Task UpdateProfile()
        {
            await _httpClient.PostAsJsonAsync<User>($"user/updateprofile/{this.UserId}", this);
            this.Message = "Profile updated successfully";
        }
        public async Task ChangePassword()
        {
            await _httpClient.PostAsJsonAsync<User>($"user/changepassword/{this.UserId}", this);
            this.Message = "Password changed successfully";
        }
        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            this.FirstName = profileViewModel.FirstName;
            this.LastName = profileViewModel.LastName;
            this.DateOfBirth = profileViewModel.DateOfBirth;
            this.EmailAddress = profileViewModel.EmailAddress;
            //add more fields
        }
        public static implicit operator ProfileViewModel(User user)
        {
            return new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserId = user.UserId,
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(ProfileViewModel profileViewModel)
        {
            return new User
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                DateOfBirth = profileViewModel.DateOfBirth,
                UserId = profileViewModel.UserId,
                EmailAddress = profileViewModel.EmailAddress,
                Password = profileViewModel.Password
            };
        }
    }
}