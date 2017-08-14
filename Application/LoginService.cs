using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application
{
    public class LoginService : ILoginService
    {
        private SignInManager<IdentityUser> _manager { get; }
        private UserManager<IdentityUser> _userManger { get; }
        public LoginService(SignInManager<IdentityUser> signInmanager, UserManager<IdentityUser> userManager)
        {
            _manager = signInmanager;
            _userManger = userManager;
        }
        public Task<SignInResult> Login(string username, string password)
        {
            return _manager.PasswordSignInAsync(username, password, false, false);
        }

        public Task<IdentityResult> Register(IdentityUser user, string password)
        {
            return _userManger.CreateAsync(user, password);
        }
    }
}
