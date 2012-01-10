using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcGrabBag.Web.Helpers
{
    public static class HtmlFormHelper
    {
        public static MvcHtmlString TooltipFor<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var tooltip = new TagBuilder("img");
            tooltip.MergeAttribute("src", UrlHelper.GenerateContentUrl("~/content/tooltip_1.gif", html.ViewContext.HttpContext));
            tooltip.AddCssClass("tooltip");
            tooltip.MergeAttribute("title", metadata.Description);
            return MvcHtmlString.Create(tooltip.ToString());
        }

        public static MvcHtmlString FullFieldEditor<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression)
        {
            return FullFieldEditor(html, expression, null);
        }

        public static MvcHtmlString FullFieldEditor<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (!metadata.ShowForEdit)
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.HideSurroundingHtml)
            {
                return html.EditorFor(expression);
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("field-wrapper");
            wrapper.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);

            var label = new TagBuilder("div");
            label.AddCssClass("field-label");
            if (metadata.IsRequired && !metadata.IsReadOnly)
            {
                // No need to show required asterisk for booleans
                if (metadata.ModelType != typeof (bool))
                {
                    label.InnerHtml += "* ";
                    wrapper.AddCssClass("required");
                }
            }

            label.InnerHtml += html.LabelFor(expression);
            wrapper.InnerHtml += label;

            var input = new TagBuilder("div");
            input.AddCssClass("field-input");
            input.InnerHtml += html.EditorFor(expression);

            if (!string.IsNullOrEmpty(metadata.Description))
            {
                input.InnerHtml += html.TooltipFor(expression);
            }

            input.InnerHtml += html.ValidationMessageFor(expression);

            wrapper.InnerHtml += input;

            return MvcHtmlString.Create(wrapper + "\r\n");
        }

        public static MvcHtmlString FullFieldDisplay<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression)
        {
            return FullFieldDisplay(html, expression, null);
        }

        public static MvcHtmlString FullFieldDisplay<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (!metadata.ShowForEdit)
            {
                return MvcHtmlString.Empty;
            }

            if(metadata.HideSurroundingHtml)
            {
                return html.DisplayFor(expression);
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("field-wrapper");
            wrapper.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);

            var label = new TagBuilder("div");
            label.AddCssClass("field-label");
            label.InnerHtml += html.LabelFor(expression);
            wrapper.InnerHtml += label;

            var input = new TagBuilder("div");
            input.AddCssClass("field-input");
            input.InnerHtml += html.DisplayFor(expression);
            wrapper.InnerHtml += input;

            return MvcHtmlString.Create(wrapper + "\r\n");
        }
    }
}