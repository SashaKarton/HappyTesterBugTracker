using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
