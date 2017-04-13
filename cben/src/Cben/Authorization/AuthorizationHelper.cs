using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Cben.Application.Features;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.Localization;
using Cben.Reflection;
using Cben.Runtime.Session;

namespace Cben.Authorization
{
    internal class AuthorizationHelper : IAuthorizationHelper, ITransientDependency
    {
        public ICbenSession CbenSession { get; set; }
        public IPermissionChecker PermissionChecker { get; set; }
        public IFeatureChecker FeatureChecker { get; set; }
        public ILocalizationManager LocalizationManager { get; set; }

        private readonly IFeatureChecker _featureChecker;
        private readonly IAuthorizationConfiguration _configuration;

        public AuthorizationHelper(IFeatureChecker featureChecker, IAuthorizationConfiguration configuration)
        {
            _featureChecker = featureChecker;
            _configuration = configuration;
            CbenSession = NullCbenSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public async Task AuthorizeAsync(IEnumerable<ICbenAuthorizeAttribute> authorizeAttributes)
        {
            if (!_configuration.IsEnabled)
            {
                return;
            }

            if (!CbenSession.UserId.HasValue)
            {
                throw new CbenAuthorizationException(
                    LocalizationManager.GetString(CbenConsts.LocalizationSourceName, "CurrentUserDidNotLoginToTheApplication")
                    );
            }

            foreach (var authorizeAttribute in authorizeAttributes)
            {
                await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo)
        {
            if (!_configuration.IsEnabled)
            {
                return;
            }

            if (AllowAnonymous(methodInfo))
            {
                return;
            }
            
            //Authorize
            await CheckFeatures(methodInfo);
            await CheckPermissions(methodInfo);
        }

        private async Task CheckFeatures(MethodInfo methodInfo)
        {
            var featureAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType<RequiresFeatureAttribute>(
                    methodInfo
                    );

            if (featureAttributes.Count <= 0)
            {
                return;
            }

            foreach (var featureAttribute in featureAttributes)
            {
                await _featureChecker.CheckEnabledAsync(featureAttribute.RequiresAll, featureAttribute.Features);
            }
        }

        private async Task CheckPermissions(MethodInfo methodInfo)
        {
            var authorizeAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType(
                    methodInfo
                ).OfType<ICbenAuthorizeAttribute>().ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }

        private static bool AllowAnonymous(MethodInfo methodInfo)
        {
            return ReflectionHelper.GetAttributesOfMemberAndDeclaringType(methodInfo)
                .OfType<ICbenAllowAnonymousAttribute>().Any();
        }
    }
}