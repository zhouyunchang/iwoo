using System;

namespace Cben.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="CbenAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class CbenAllowAnonymousAttribute : Attribute, ICbenAllowAnonymousAttribute
    {

    }
}