using Application;
using Microsoft.AspNetCore.Hosting;

namespace Nancy.ContainerSharing
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IHostingEnvironment env, ILoginService loginService)
        {
           Get("/",_ => env.EnvironmentName);
        }
    }
}
