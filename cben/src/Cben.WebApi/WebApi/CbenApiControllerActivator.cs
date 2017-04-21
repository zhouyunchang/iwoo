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


        /// <summary>
        /// WebApi Controller
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)_iocResolver.Resolve(controllerType);

            request.RegisterForDispose(
                new DisposeAction(
                        () => _iocResolver.Release(controller)));

            return controller;
        }
    }
}