using System.Web;

namespace Cben.Web.Localization
{
    public interface ICurrentCultureSetter
    {
        void SetCurrentCulture(HttpContext httpContext);
    }
}
