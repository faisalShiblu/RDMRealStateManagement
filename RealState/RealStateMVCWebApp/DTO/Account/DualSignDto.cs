using System.ComponentModel.DataAnnotations;

namespace RealStateMVCWebApp.DTO.Account
{
    public class DualSignDto
    {
        public string? Name { get; set; } 
        public string? Email { get; set; }  
        public string? Password { get; set; }  

        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }  

        [Display(Name = "Mobile")]
        public string? PhoneNumber { get; set; }  

        [Display(Name = "Email")]
        public string? LogInEmail { get; set; }  

        [Display(Name = "Password")]
        public string? LogInPassword { get; set; }  
    }
}
