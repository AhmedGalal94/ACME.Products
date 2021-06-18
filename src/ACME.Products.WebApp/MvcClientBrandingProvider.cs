using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;
namespace ACME.Products.WebApp
{

    [Dependency(ReplaceServices = true)]
    public class MvcClientBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Products Mvc Client";
    }
}

