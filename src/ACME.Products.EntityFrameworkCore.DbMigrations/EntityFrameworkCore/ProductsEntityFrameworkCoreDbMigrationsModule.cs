using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ACME.Products.EntityFrameworkCore
{
    [DependsOn(
        typeof(ProductsEntityFrameworkCoreModule)
        )]
    public class ProductsEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProductsMigrationsDbContext>();
        }
    }
}
