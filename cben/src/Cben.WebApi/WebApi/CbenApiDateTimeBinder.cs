using Cben.Timing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Cben.WebApi
{
    /// <summary>
    /// Binds datetime values from api requests to model
    /// </summary>
    public class CbenApiDateTimeBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var date = value?.ConvertTo(typeof(DateTime?), CultureInfo.CurrentCulture) as DateTime?;
            if (date != null)
            {
                bindingContext.Model = Clock.Normalize(date.Value);
            }

            return true;
        }
    }
}