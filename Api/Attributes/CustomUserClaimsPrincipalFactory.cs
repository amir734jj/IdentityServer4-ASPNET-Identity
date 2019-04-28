using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.Models;

namespace Api.Attributes
{
    public class CustomUserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<User>
    {
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await CreateAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.Fullname),
                new Claim(ClaimTypes.Surname, user.Fullname)

            });
            return principal;
        }
    }
}