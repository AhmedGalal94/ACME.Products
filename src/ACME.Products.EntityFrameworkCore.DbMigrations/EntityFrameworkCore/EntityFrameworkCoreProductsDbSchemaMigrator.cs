using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ACME.Products.Data;
using Volo.Abp.DependencyInjection;

namespace ACME.Products.EntityFrameworkCore
{
    public class EntityFrameworkCoreProductsDbSchemaMigrator
        : IProductsDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreProductsDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ProductsMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ProductsMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}