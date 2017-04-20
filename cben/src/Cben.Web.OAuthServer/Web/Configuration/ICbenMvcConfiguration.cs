using Cben.Domain.Uow;
using Cben.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Web.Configuration
{
    public interface ICbenMvcConfiguration
    {
        /// <summary>
        /// Default UnitOfWorkAttribute for all actions.
        /// </summary>
        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        /// <summary>
        /// Default WrapResultAttribute for all actions.
        /// </summary>
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        string DomainFormat { get; set; }

        string LocalizationCookieName { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// </summary>
        bool IsAuditingEnabled { get; set; }

        /// <summary>
        /// Used to enable/disable auditing for child MVC actions.
        /// Default: false.
        /// </summary>
        bool IsAuditingEnabledForChildActions { get; set; }

        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (Cben hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }


    }
}
