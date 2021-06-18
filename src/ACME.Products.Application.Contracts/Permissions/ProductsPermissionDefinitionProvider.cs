using ACME.Products.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ACME.Products.Permissions
{
    public class ProductsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ProductsGroup = context.AddGroup(ProductsPermissions.GroupName);

            //Define ProductsPermissions
            var ProductsPermission = ProductsGroup.AddPermission(ProductsPermissions.Products.Default, L("Permission:ProductsManagements"));
            ProductsPermission.AddChild(ProductsPermissions.Products.Create, L("Permission:ProductsManagements.Create"));
            ProductsPermission.AddChild(ProductsPermissions.Products.Edit, L("Permission:ProductsManagements.Edit"));
            ProductsPermission.AddChild(ProductsPermissions.Products.Delete, L("Permission:ProductsManagements.Delete"));

            //Define CategoriesPermissions
            var CategoriesPermission = ProductsGroup.AddPermission(ProductsPermissions.Categories.Default, L("Permission:Categories"));
            CategoriesPermission.AddChild(ProductsPermissions.Categories.Create, L("Permission:Categories.Create"));
            CategoriesPermission.AddChild(ProductsPermissions.Categories.Edit, L("Permission:Categories.Edit"));
            CategoriesPermission.AddChild(ProductsPermissions.Categories.Delete, L("Permission:Categories.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProductsResource>(name);
        }
    }
}
