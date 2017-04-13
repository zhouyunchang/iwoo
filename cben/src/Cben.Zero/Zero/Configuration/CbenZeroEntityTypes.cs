using System;
using Cben.Authorization.Roles;
using Cben.Authorization.Users;
using Cben.MultiTenancy;

namespace Cben.Zero.Configuration
{
    public class CbenZeroEntityTypes : ICbenZeroEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof (CbenUserBase).IsAssignableFrom(value))
                {
                    throw new CbenException(value.AssemblyQualifiedName + " should be derived from " + typeof(CbenUserBase).AssemblyQualifiedName);
                }

                _user = value;
            }
        }
        private Type _user;

        public Type Role
        {
            get { return _role; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(CbenRoleBase).IsAssignableFrom(value))
                {
                    throw new CbenException(value.AssemblyQualifiedName + " should be derived from " + typeof(CbenRoleBase).AssemblyQualifiedName);
                }

                _role = value;
            }
        }
        private Type _role;

        public Type Tenant
        {
            get { return _tenant; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(CbenTenantBase).IsAssignableFrom(value))
                {
                    throw new CbenException(value.AssemblyQualifiedName + " should be derived from " + typeof(CbenTenantBase).AssemblyQualifiedName);
                }

                _tenant = value;
            }
        }
        private Type _tenant;
    }
}