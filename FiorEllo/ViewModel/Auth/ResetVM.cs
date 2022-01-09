using System.ComponentModel.DataAnnotations;

namespace FiorEllo.ViewModel.Auth
{
    public class ResetVM
    {
        [Required(ErrorMessage ="email daxil edin"), MaxLength(255,ErrorMessage = "255 simvoldan atriq daxil ede bilmezsiz"),DataType(DataType.EmailAddress,ErrorMessage = "email tipinden daxil edin")]
        public string Email { get; set; }
    }
}