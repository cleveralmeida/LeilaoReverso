using LR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public MyUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor, 
        LeilaoReversoContext context
        )
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        await UserManager.RemoveClaimsAsync(user, identity.Claims);
        var roles = await UserManager.GetRolesAsync(user);
        if (roles.Count == 0)
            identity.AddClaim(new Claim("Perfil", ""));
        else
            identity.AddClaim(new Claim("Perfil", roles[0]));
        identity.AddClaim(new Claim("IdUsuario", user.Id));
        if (user.Nome != null)
            identity.AddClaim(new Claim("Nome", user.Nome));
        else
            identity.AddClaim(new Claim("Nome", ""));
        return identity;
    }
    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        return principal;
    }
}

 