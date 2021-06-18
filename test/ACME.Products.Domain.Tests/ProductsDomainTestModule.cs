using ACME.Products.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ACME.Products
{
    [DependsOn(
        typeof(ProductsEntityFrameworkCoreTestModule)
        )]
    public class ProductsDomainTestModule : AbpModule
    {

    }
}