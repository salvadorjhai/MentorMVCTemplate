using System.ComponentModel.DataAnnotations;

namespace MentorMVCTemplate.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your username")]
        public string username { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
        public string password { get; set; } = "";
    }
}
