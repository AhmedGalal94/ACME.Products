using System.Threading.Tasks;
using ACME.Products.Localization;
using ACME.Products.MultiTenancy;
using ACME.Products.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace ACME.Products.Web.Menus
{
    public class ProductsMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
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
            if (await context.IsGrantedAsync(ProductsPermissions.Categories.Default))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                     ProductsMenus.Categories,
                    l["Menu:Categories"],
                    url: "/Categories"
                ));
            }

            if (await context.IsGrantedAsync(ProductsPermissions.Products.Default))
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                     ProductsMenus.Products,
                    l["Menu:Products"],
                    url: "/Products"
                ));
            }

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        }
    }
}
