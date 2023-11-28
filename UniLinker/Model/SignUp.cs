using System.ComponentModel.DataAnnotations;

namespace UniLinker.Model
{
    public class SignUp
    {
        public class SignUpRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string ConfirmPassword { get; set; }
            [Required]
            public string Role { get; set; }
        }
        public class SignUpResponse
        {
            public bool isSuccess {get; set;}
            public string Message { get; set;}
        }
    }
}
