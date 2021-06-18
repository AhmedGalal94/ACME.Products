namespace ACME.Products.Permissions
{
    public static class ProductsPermissions
    {
        public const string GroupName = "Products";

        public static class Categories
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Products
        {
            public const string Default = GroupName + ".ProductsManagements";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }

   
}