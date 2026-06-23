using System.ComponentModel.DataAnnotations;

namespace InsuranceIQ.Core.ViewModels.Auth;

public class LoginViewModel
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty; 
    
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}

