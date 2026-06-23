using Microsoft.AspNetCore.Identity;

namespace InsuranceIQ.Data.Seeding;

public static class DataSeeder
{
    public static readonly string[] Roles = ["Admin", "Agent", "Policyholder"];

    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to seed role '{role}': {errors}");
                }
            }
        }
    }
}