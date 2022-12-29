using BuildingBlocks.EFCore;
using FluentPOS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Identity.API.Data;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityDataSeeder(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<long>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAllAsync()
    {
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (await _roleManager.RoleExistsAsync(Constants.Role.Admin) == false)
            await _roleManager.CreateAsync(new(Constants.Role.Admin));

        if (await _roleManager.RoleExistsAsync(Constants.Role.User) == false)
            await _roleManager.CreateAsync(new(Constants.Role.User));
    }

    private async Task SeedUsers()
    {
        if (await _userManager.FindByNameAsync("mukesh.murugan") == null)
        {
            var user = new ApplicationUser
            {
                FirstName = "Mukesh",
                LastName = "Murugan",
                UserName = "mukesh.murugan",
                Email = "mukesh.murugan@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, "123Pa$$word!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.Role.Admin);
            }
        }

        if (await _userManager.FindByNameAsync("John") == null)
        {
            var user = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "john.doe",
                Email = "john.doe@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, "123Pa$$word!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.Role.User);
            }
        }
    }
}
