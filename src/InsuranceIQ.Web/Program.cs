using InsuranceIQ.Core.Models;
using InsuranceIQ.Data;
using InsuranceIQ.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// --- Services ---
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.MigrationsAssembly("InsuranceIQ.Data")
    )
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedAccount = false;
        
    })
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
    
    //Tell identity where your custom pages live
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
    
    var app = builder.Build();
    
    // -- Seed roles on Startup ---
    using (var scope = app.Services.CreateScope())
    {
        var roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await
            DataSeeder.SeedRolesAsync(roleManager);
    }
    
    // -- Middleware pipeline --- 
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
    