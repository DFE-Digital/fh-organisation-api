using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace FamilyHubs.IdentityServerHost.Persistence.Repository;

public class SeedData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager != null)
            {
                await EnsureRoles(roleManager);
            }


            scope?.ServiceProvider?.GetService<PersistedGrantDbContext>()?.Database.Migrate();

            var context = scope?.ServiceProvider.GetService<ConfigurationDbContext>();
            context?.Database.Migrate();
            if (context != null && scope != null)
            {
                EnsureSeedData(context);
                EnsureTestUsers(scope);
                await EnsureUsers(scope);
            }
        }
    }

    private static async Task EnsureRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            Log.Debug("Roles being populated");
            string[] roles = new string[] { "DfEAdmin", "LAAdmin", "VCSAdmin", "Pro" };
            foreach (var role in roles)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
        else
        {
            Log.Debug("Roles already populated");
        }

    }

    private static void EnsureSeedData(ConfigurationDbContext context)
    {
        if (!context.Clients.Any())
        {
            Log.Debug("Clients being populated");
            foreach (var client in Config.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }

            context.SaveChanges();
        }
        else
        {
            Log.Debug("Clients already populated");
        }

        if (!context.IdentityResources.Any())
        {
            Log.Debug("IdentityResources being populated");
            foreach (var resource in Config.IdentityResources.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
        else
        {
            Log.Debug("IdentityResources already populated");
        }

        if (!context.ApiScopes.Any())
        {
            Log.Debug("ApiScopes being populated");
            foreach (var resource in Config.ApiScopes.ToList())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
        else
        {
            Log.Debug("ApiScopes already populated");
        }

        if (!context.ApiResources.Any())
        {
            Log.Debug("ApiResources being populated");
            foreach (var resource in Config.ApiResources.ToList())
            {
                context.ApiResources.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
        else
        {
            Log.Debug("ApiResources already populated");
        }

        if (!context.IdentityProviders.Any())
        {
            Log.Debug("OIDC IdentityProviders being populated");
            context.IdentityProviders.Add(new OidcProvider
            {
                Scheme = "demoidsrv",
                DisplayName = "IdentityServer",
                Authority = "https://demo.duendesoftware.com",
                ClientId = "login",
            }.ToEntity());
            context.SaveChanges();
        }
        else
        {
            Log.Debug("OIDC IdentityProviders already populated");
        }
    }

    private static async Task EnsureUsers(IServiceScope scope)
    {

        string[] LAAdmins = new string[] { "BtlLAAdmin", "LanLAAdmin", "LbrLAAdmin", "SalLAAdmin", "SufLAAdmin", "TowLAAdmin" };
        string[] SvcAdmins = new string[] { "BtlVCSAdmin", "LanVCSAdmin", "LbrVCSAdmin", "SalVCSAdmin", "SufVCSAdmin", "TowVCSAdmin" };
        string[] Pro = new string[] { "BtlPro", "LanPro", "LbrPro", "SalPro", "SufPro", "TowPro" };
        string[] Websites = new string[] { "https://www.bristol.gov.uk/", "https://www.lancashire.gov.uk/", "https://www.redbridge.gov.uk/", "https://www.salford.gov.uk/", "https://www.suffolk.gov.uk/", "https://www.towerhamlets.gov.uk/Home.aspx" };

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        await AddUser(userMgr, "DfEAdmin", "Pass123$", "DfEAdmin", "www.warmhandover.gov.uk");
        for (int i = 0; i < LAAdmins.Length; i++)
        {
            await AddUser(userMgr, LAAdmins[i], "Pass123$", "LAAdmin", Websites[i]);
        }
        for (int i = 0; i < SvcAdmins.Length; i++)
        {
            await AddUser(userMgr, SvcAdmins[i], "Pass123$", "VCSAdmin", Websites[i]);
        }
        for (int i = 0; i < Pro.Length; i++)
        {
            await AddUser(userMgr, Pro[i], "Pass123$", "Pro", Websites[i]);
        }
    }

    private static async Task AddUser(UserManager<IdentityUser> userMgr, string person, string password, string role, string website)
    {
        var user = userMgr.FindByNameAsync(person).Result;
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = person,
                Email = $"{person}@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(user, new Claim[]
            {
        new Claim(JwtClaimTypes.Name, person),
        new Claim(JwtClaimTypes.GivenName, person),
        //new Claim(JwtClaimTypes.FamilyName, "Smith"),
        new Claim(JwtClaimTypes.WebSite, "http://warmhandover.gov.uk"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await userMgr.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug($"{person} created");
        }
        else
        {
            Log.Debug($"{person} already exists");
        }
    }

    private static void EnsureTestUsers(IServiceScope scope)
    {
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice == null)
        {
            alice = new IdentityUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice, new Claim[]
            {
        new Claim(JwtClaimTypes.Name, "Alice Smith"),
        new Claim(JwtClaimTypes.GivenName, "Alice"),
        new Claim(JwtClaimTypes.FamilyName, "Smith"),
        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            //result = userMgr.AddToRoleAsync

            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
            bob = new IdentityUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, new Claim[]
            {
        new Claim(JwtClaimTypes.Name, "Bob Smith"),
        new Claim(JwtClaimTypes.GivenName, "Bob"),
        new Claim(JwtClaimTypes.FamilyName, "Smith"),
        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
        new Claim("location", "somewhere")
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        }
    }
}
