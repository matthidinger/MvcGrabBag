using System;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Helpers
{
    public static class SelectorHelper
    {
        /// <summary>
        /// Get the raw value from model state
        /// </summary>
        public static T GetModelStateValue<T>(this HtmlHelper helper, string key)
        {
            ModelState modelState;
            if (helper.ViewData.ModelState.TryGetValue(key, out modelState) && modelState.Value != null)
                return (T)modelState.Value.ConvertTo(typeof(T), null);
            return default(T);
        }
    }
}