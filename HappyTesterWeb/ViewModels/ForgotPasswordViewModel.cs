using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

    }
}
