using System;
using Cben.Authorization.Users;

namespace Cben.Runtime.Session
{
    public static class CbenSessionExtensions
    {
        public static bool IsUser(this ICbenSession session, CbenUserBase user)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return session.TenantId == user.TenantId && 
                session.UserId.HasValue && 
                session.UserId.Value == user.Id;
        }
    }
}
