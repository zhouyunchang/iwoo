using Cben.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Cben.WebApi
{
    public class CbenApiControllerActivator : IHttpControllerActivator
    {
        private readonly IIocResolver _iocResolver;

        public CbenApiControllerActivator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}