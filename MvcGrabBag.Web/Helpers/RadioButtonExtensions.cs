using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MvcGrabBag.Web.Helpers
{
    public static class RadioButtonExtensions
    {
        public static MvcHtmlString RadioButtonListFor<TModel, TRadioButtonListValue>(this HtmlHelper<TModel> htmlHelper,
                                                                               Expression<Func<TModel, RadioButtonListViewModel<TRadioButtonListValue>>> expression)
            where TModel : class
        {
            return htmlHelper.RadioButtonListFor(expression, null);
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TRadioButtonListValue>(this HtmlHelper<TModel> htmlHelper,
                                                                               Expression<Func<TModel, RadioButtonListViewModel<TRadioButtonListValue>>> expression,
                                                                               object htmlAttributes)
            where TModel : class
        {
            return htmlHelper.RadioButtonListFor(expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TRadioButtonListValue>(this HtmlHelper<TModel> htmlHelper,
                                                                               Expression<Func<TModel, RadioButtonListViewModel<TRadioButtonListValue>>> expression,
                                                                               IDictionary<string, object> htmlAttributes) where TModel : class
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            var inputName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            var uniqueId = inputName + "_" + Guid.NewGuid();
            var radioButtonList = GetValue(htmlHelper, expression);
            if (radioButtonList == null)
                return MvcHtmlString.Empty;

            if (radioButtonList.ListItems == null)
                return MvcHtmlString.Empty;

            var divTag = new TagBuilder("div");
            divTag.MergeAttribute("id", uniqueId);
            divTag.MergeAttribute("class", "radio");
            foreach (var item in radioButtonList.ListItems)
            {
                htmlAttributes["title"] = item.Tooltip;

                var radioButtonTag = RadioButton(htmlHelper, inputName, uniqueId,
                                                 new SelectListItem
                                                     {
                                                         Text = item.Text,
                                                         Selected = item.Selected,
                                                         Value = item.Value.ToString()
                                                     }, htmlAttributes);

                divTag.InnerHtml += radioButtonTag;
            }


            var validationMessage = htmlHelper.ValidationMessage(inputName, "*");
            if (!MvcHtmlString.IsNullOrEmpty(validationMessage))
            {
                return new MvcHtmlString(divTag + validationMessage.ToHtmlString());
            }
            return new MvcHtmlString(divTag.ToString());
        }

        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, string uniqueId,
                                         SelectListItem listItem,
                                         IDictionary<string, object> htmlAttributes)
        {
            var inputIdSb = new StringBuilder();
            inputIdSb.Append(name)
                .Append("_")
                .Append(listItem.Value)
                .Append("_")
                .Append(uniqueId);

            var sb = new StringBuilder();

            var builder = new TagBuilder("input");
            if (listItem.Selected) builder.MergeAttribute("checked", "checked");
            builder.MergeAttribute("type", "radio");
            builder.MergeAttribute("value", listItem.Value);
            builder.MergeAttribute("id", inputIdSb.ToString());
            builder.MergeAttribute("name", name + ".SelectedValue");
            builder.MergeAttributes(htmlAttributes);
            sb.Append(builder.ToString(TagRenderMode.SelfClosing));
            sb.Append(RadioButtonLabel(inputIdSb.ToString(), listItem.Text, htmlAttributes));
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString RadioButtonLabel(string inputId, string displayText, IDictionary<string, object> htmlAttributes)
        {
            var labelBuilder = new TagBuilder("label");
            labelBuilder.MergeAttribute("for", inputId);
            labelBuilder.MergeAttributes(htmlAttributes);
            labelBuilder.InnerHtml = displayText;
            return new MvcHtmlString(labelBuilder.ToString(TagRenderMode.Normal));
        }


        public static TProperty GetValue<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            where TModel : class
        {
            TModel model = htmlHelper.ViewData.Model;
            if (model == null)
            {
                return default(TProperty);
            }
            var func = expression.Compile();
            return func(model);
        }
    }
}