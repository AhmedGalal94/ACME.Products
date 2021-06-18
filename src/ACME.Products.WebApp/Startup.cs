using Castle.Core.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Products.WebApp
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ProductsMvcClientModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }


        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}


        //public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllersWithViews();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }
        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllerRoute(
        //            name: "default",
        //            pattern: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}

        //private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddAuthentication()
        //         .AddCookie("Cookies", options =>
        //          {
        //              options.ExpireTimeSpan = TimeSpan.FromDays(365);
        //          })
        //        .AddOpenIdConnect("oidc", options =>
        //        {
        //            options.Authority = configuration["AuthServer:Authority"];
        //            options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
        //            options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
        //            options.ClientId = configuration["AuthServer:ClientId"];
        //            options.ClientSecret = configuration["AuthServer:ClientSecret"];
        //            options.SaveTokens = true;
        //            options.GetClaimsFromUserInfoEndpoint = true;
        //            options.Scope.Add("role");
        //            options.Scope.Add("email");
        //            options.Scope.Add("phone");
        //            options.Scope.Add("Products");
        //        });
        //}
    }
}
