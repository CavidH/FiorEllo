using System.ComponentModel.DataAnnotations;

namespace FiorEllo.ViewModel.Auth
{
    public class LoginVM
    {
        [Required, MaxLength(255),DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}