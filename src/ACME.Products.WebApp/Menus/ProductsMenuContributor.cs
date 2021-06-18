using System;
using System.Threading.Tasks;
using ACME.Products.Localization;
using ACME.Products.MultiTenancy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace ACME.Products.WebApp.Menus
{
    public class ProductsMenuContributor : IMenuContributor
    {

        private readonly IConfiguration _configuration;

        public ProductsMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<ProductsResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    ProductsMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );
            context.Menu.AddItem(new ApplicationMenuItem(
                    ProductsMenus.Products,
                    l["Menu:Products"],
                    url: "/Products"
                ));
        }
        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<ProductsResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            if (currentUser.IsAuthenticated)
            {
                context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));
            }
            return Task.CompletedTask;
        }
    }
}
