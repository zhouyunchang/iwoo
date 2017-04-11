using Cben.Application.Navigation;
using Cben.Collections;

namespace Cben.Configuration.Startup
{
    internal class NavigationConfiguration : INavigationConfiguration
    {
        public ITypeList<NavigationProvider> Providers { get; private set; }

        public NavigationConfiguration()
        {
            Providers = new TypeList<NavigationProvider>();
        }
    }
}