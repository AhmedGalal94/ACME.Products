using System.Threading.Tasks;

namespace ACME.Products.Data
{
    public interface IProductsDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
