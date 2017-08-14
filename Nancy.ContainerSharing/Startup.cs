using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using StructureMap;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Application;

namespace Nancy.ContainerSharing
{
    public class Startup
    {
        private IConfiguration config;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json");
            config = builder.Build();

        }
        private IContainer _container;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
     
            services.AddDbContext<IdentityDbContext<IdentityUser>>(options =>
                 options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityDbContext<IdentityUser>>();

            _container = new Container();
            _container.Configure(cfg => {
                cfg.Scan(x => {
                    x.WithDefaultConventions();
                    x.AssemblyContainingType<ILoginService>();
                });
            });
            _container.Populate(services);
            return _container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            InitDatabase(app);
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentity();

            app.UseOwin(o => o.UseNancy(b => b.Bootstrapper = new Bootstrapper(_container)));
        }
        private void InitDatabase(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<IdentityDbContext<IdentityUser>>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
