using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Cben.Authorization
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<ICbenAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo);
    }
}