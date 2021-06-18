using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ACME.Products.Web
{
    [Dependency(ReplaceServices = true)]
    public class ProductsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Products";
    }
}
