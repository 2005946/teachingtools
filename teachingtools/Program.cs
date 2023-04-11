using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using teachingtools.Data;
using System.Runtime.CompilerServices;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Index");
    options.AccessDeniedPath = new PathString("/AccessDenied");
    options.LogoutPath = new PathString("/Index");
}
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "Subscriber", "Member" };
    foreach (var roleName in roleNames)
    {
        bool roleExists = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
    var _user = await UserManager.FindByNameAsync("2005946@chester.ac.uk");
    if (_user != null)
    {
        await UserManager.AddToRoleAsync(_user, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
