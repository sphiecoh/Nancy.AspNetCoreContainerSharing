using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application
{
    public interface ILoginService
    {
        Task<SignInResult> Login(string username, string password);
        Task<IdentityResult> Register(IdentityUser user, string password);

    }
}
