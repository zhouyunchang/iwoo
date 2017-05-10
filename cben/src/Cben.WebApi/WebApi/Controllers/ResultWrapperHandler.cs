using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Cben.Dependency;
using Cben.Extensions;
using Cben.Web.Models;
using Cben.WebApi.Configuration;
using Cben.WebApi.Models;

namespace Cben.WebApi.Controllers
{
    /// <summary>
    /// Wraps Web API return values by <see cref="AjaxResponse"/>.
    /// </summary>
    public class ResultWrapperHandler : DelegatingHandler, ITransientDependency
    {
        private readonly ICbenWebApiConfiguration _configuration;

        public ResultWrapperHandler(ICbenWebApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            WrapResultIfNeeded(request, result);
            return result;
        }

        protected virtual void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            if (_configuration.SetNoCacheForAllResponses)
            {
                //Based on http://stackoverflow.com/questions/49547/making-sure-a-web-page-is-not-cached-across-all-browsers
                response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MaxAge = TimeSpan.Zero,
                    MustRevalidate = true
                };
            }

            var wrapAttr = HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(request.GetActionDescriptor())
                           ?? _configuration.DefaultWrapResultAttribute;

            if (!wrapAttr.WrapOnSuccess)
            {
                return;
            }

            if (IsIgnoredUrl(request.RequestUri))
            {
                return;
            }

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is AjaxResponseBase)
            {
                return;
            }

            response.Content = new ObjectContent<AjaxResponse>(
                new AjaxResponse(resultObject),
                _configuration.HttpConfiguration.Formatters.JsonFormatter
                );
        }

        private bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _configuration.ResultWrappingIgnoreUrls
                .Any(url => uri.AbsolutePath.StartsWith(url, StringComparison.OrdinalIgnoreCase));
        }
    }
}