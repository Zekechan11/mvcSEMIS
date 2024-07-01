using Microsoft.EntityFrameworkCore;
using semissssloan.Entities;
using semissssloan.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PelayosemisContext>( options => 
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LawasUtangananT;TrustServerCertificate=true;Trusted_Connection=True")
) ;

// Configure IdentityContext
builder.Services.AddDbContext<IdentityContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("localhost\\SQLEXPRESS;Database=LawasUtangananT;TrustServerCertificate=true;Trusted_Connection=True")));

builder.Services.AddIdentity<AppUser, AppRole>(
    options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        // options.Lockout.MaxFailedAccessAttempts = 5;
        // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
    }
)
	.AddEntityFrameworkStores<IdentityContext>()
	.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();
