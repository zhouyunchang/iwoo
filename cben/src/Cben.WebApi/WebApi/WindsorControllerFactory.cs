﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cben.Dependency;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Cben.WebApi
{
    /// <summary>
    /// This class is used to allow MVC to use dependency injection system while creating MVC controllers.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory, IHttpControllerActivator
    {
        /// <summary>
        /// Reference to DI kernel.
        /// </summary>
        private readonly IIocResolver _iocManager;

        /// <summary>
        /// Creates a new instance of WindsorControllerFactory.
        /// </summary>
        /// <param name="iocManager">Reference to DI kernel</param>
        public WindsorControllerFactory(IIocResolver iocManager)
        {
            _iocManager = iocManager;
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
            var controller = (IHttpController)_iocManager.Resolve(controllerType);

            request.RegisterForDispose(
                new DisposeAction(
                        () => _iocManager.Release(controller)));

            return controller;
        }

        /// <summary>
        /// Called by MVC system and releases/disposes given controller instance.
        /// </summary>
        /// <param name="controller">Controller instance</param>
        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }

        /// <summary>
        /// Called by MVC system and creates controller instance for given controller type.
        /// </summary>
        /// <param name="requestContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return _iocManager.Resolve<IController>(controllerType);
        }
    }
}