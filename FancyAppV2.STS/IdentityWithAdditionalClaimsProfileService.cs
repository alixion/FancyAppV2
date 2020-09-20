using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using FancyAppV2.STS.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4;

namespace FancyAppV2.STS
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityWithAdditionalClaimsProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            
            claims.RemoveAll(x => x.Type == JwtClaimTypes.Name);
            claims.Add(new Claim(JwtClaimTypes.Name,user.Name));
            

            if (user.IsAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles!=null && roles.Any())
            {
                claims.AddRange(roles.Select(x=>new Claim(JwtClaimTypes.Role,x)));
            }

            claims.Add(user.TwoFactorEnabled ? new Claim("amr", "mfa") : new Claim("amr", "pwd"));

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));
            

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}