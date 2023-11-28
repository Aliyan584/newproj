using System.ComponentModel.DataAnnotations;

namespace UniLinker.Model
{
    public class SignInRequest

    {
        [Required]
        [EmailAddress]
        public String Email{ get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String Role {get; set; }
    }

    public class SignInResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
