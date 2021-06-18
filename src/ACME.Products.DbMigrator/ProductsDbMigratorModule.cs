using ACME.Products.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ACME.Products.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ProductsEntityFrameworkCoreDbMigrationsModule),
        typeof(ProductsApplicationContractsModule)
        )]
    public class ProductsDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
