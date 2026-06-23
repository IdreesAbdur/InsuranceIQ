using Microsoft.AspNetCore.Identity;

namespace InsuranceIQ.Core.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public string FullName => $"{FirstName} {LastName}".Trim();
}