using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IWantApp.Services
{
    public class UserCreator
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserCreator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(IdentityResult, string)> Create(string Email, string password, List<Claim> claims)
        {
            var newUser = new IdentityUser() { UserName = Email, Email = Email };
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded) return (result, string.Empty);

            return (await _userManager.AddClaimsAsync(newUser, claims), newUser.Id);
        }
        
    }
}