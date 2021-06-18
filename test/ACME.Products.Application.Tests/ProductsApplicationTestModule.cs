using Volo.Abp.Modularity;

namespace ACME.Products
{
    [DependsOn(
        typeof(ProductsApplicationModule),
        typeof(ProductsDomainTestModule)
        )]
    public class ProductsApplicationTestModule : AbpModule
    {

    }
}